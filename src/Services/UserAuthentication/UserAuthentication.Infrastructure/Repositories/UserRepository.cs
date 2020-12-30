using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AutoMapper;
using eShopWithReact.Services.UserAuthentication.Core.Entities;
using eShopWithReact.Services.UserAuthentication.Core.Helpers;
using eShopWithReact.Services.UserAuthentication.Core.Interfaces;
using eShopWithReact.Services.UserAuthentication.Core.Models;
using eShopWithReact.Services.UserAuthentication.Infrastructure.Settings;
using eShopWithReact.Services.UserAuthentication.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace eShopWithReact.Services.UserAuthentication.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwt;
        private readonly ITokenRepository _tokenRepository;

        public UserRepository(
            UserDbContext context, 
            UserManager<ApplicationUser> userManager, 
            IMapper mapper,
            IOptions<JwtSettings> jwt,
            ITokenRepository tokenRepository
            )
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _jwt = jwt.Value;
            _tokenRepository = tokenRepository;
        }

        public async Task<RegisterResponse> Register(RegisterRequest model)
        {
            RegisterResponse response = new RegisterResponse();

            var user = _mapper.Map<ApplicationUser>(model);

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail != null)
            {
                response.Success = false;
                response.Message = $"Email {user.Email } is already registered.";
            }
            else
            {
                user.UserName = model.Email;
                user.Created = DateTime.UtcNow;
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    response.FirstName = user.FirstName;
                    response.LastName = user.LastName;
                    response.Email = user.Email;
                    response.Success = true;
                    response.Message = "Registration successful, please check your email for verification instructions";

                    // Set Default Role
                    await _userManager.AddToRoleAsync(user, UserRole.User);

                    response.Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Unable to register user with email: {user.Email}";
                }
            }
            return response;
        }

        public async Task<bool> VerifyEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null) return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest, string ipAddress)
        {
            var authenticationResponse = new AuthenticateResponse();

            var user = await _userManager.FindByEmailAsync(authenticateRequest.Email);

            if (user == null)
            {
                authenticationResponse.IsAuthenticated = false;
                authenticationResponse.Message = $"No Accounts Registered with {authenticateRequest.Email}.";
                return authenticationResponse;
            }

            if (await _userManager.CheckPasswordAsync(user, authenticateRequest.Password))
            {
                authenticationResponse.IsAuthenticated = true;

                // authentication successful so generate jwt and refresh tokens
                var jwtToken = await Task.Run(() => _tokenRepository.GenerateJwtToken(user));
                var refreshToken = await Task.Run(() => _tokenRepository.GenerateRefreshToken(ipAddress));
                user.RefreshTokens.Add(refreshToken);

                // remove old refresh tokens from account
                removeOldRefreshTokens(user);

                _context.Update(user);
                _context.SaveChanges();

                authenticationResponse = _mapper.Map<AuthenticateResponse>(user);
                authenticationResponse.Token = jwtToken;
                authenticationResponse.RefreshToken = refreshToken.Token;

                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationResponse.Roles = rolesList.ToList();

                return authenticationResponse;
            }

            authenticationResponse.IsAuthenticated = false;
            authenticationResponse.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationResponse;
        }

        public async Task<string> AddRoleAsync(AddRole addRole)
        {
            var user = await _userManager.FindByEmailAsync(addRole.Email);
            if (user == null)
            {
                return $"No Accounts Registered with {addRole.Email}.";
            }

            if (await _userManager.CheckPasswordAsync(user, addRole.Password))
            {
                Type type = typeof(UserRole);
                var validRole = type.GetFields().Select(x => x.GetValue(addRole.Role).ToString()).FirstOrDefault();
                if (!string.IsNullOrEmpty(validRole))
                {
                    await _userManager.AddToRoleAsync(user, validRole);
                    return $"Added {addRole.Role} to user {addRole.Email}.";
                }
                return $"Role {addRole.Role} not found.";
            }
            return $"Incorrect Credentials for user {user.Email}.";
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var authenticateResponse = new AuthenticateResponse();
            var (refreshToken, user) = getOldRefreshToken(token);

            if (user == null)
            {
                authenticateResponse.IsAuthenticated = false;
                authenticateResponse.Message = $"Token did not match any users.";
                return authenticateResponse;
            }

            // replace old refresh token with a new one and save
            var newRefreshToken = _tokenRepository.GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(user);

            _context.Update(user);
            _context.SaveChanges();

            // generate new jwt
            var jwtToken = await _tokenRepository.GenerateJwtToken(user);

            authenticateResponse = _mapper.Map<AuthenticateResponse>(user);
            authenticateResponse.IsAuthenticated = true;
            authenticateResponse.Token = jwtToken;
            authenticateResponse.RefreshToken = newRefreshToken.Token;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticateResponse.Roles = rolesList.ToList();
            return authenticateResponse;
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var (refreshToken, user) = getOldRefreshToken(token);

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public void ValidateResetToken(ValidateResetTokenRequest model)
        {
            var user = _context.Users.SingleOrDefault(x =>
                x.ResetToken == model.Token &&
                x.ResetTokenExpires > DateTime.UtcNow);

            if (user == null)
                throw new AppException("Invalid token");
        }


        public async Task<string> ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var user = await _userManager.FindByEmailAsync(model.Email); ;

            // always return ok response to prevent email enumeration
            if (user == null) return string.Empty;

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new AppException("Invalid token");

            await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        }

        public async Task ChangePassword(ChangePasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new AppException("Invalid token");

            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IList<AccountResponse>>(users);
        }

        public async Task<AccountResponse> GetById(string id)
        {
            var user = await getAccount(id);

            return _mapper.Map<AccountResponse>(user);
        }

        public async Task<AccountResponse> Update(string id, UpdateRequest model)
        {
            var user = await getAccount(id);

            // validate
            if (user.Email != model.Email && _context.Users.Any(x => x.Email == model.Email))
                throw new AppException($"Email '{model.Email}' is already Registered");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.Email;
            user.Email = model.Email;
            user.Updated = DateTime.UtcNow;

            var resetPassResult = await _userManager.UpdateAsync(user);

            var response = _mapper.Map<AccountResponse>(user);
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();

            return response;
        }

        public async Task Delete(string id)
        {
            var user = await getAccount(id);
            await _userManager.DeleteAsync(user);
        }

        // ######################################## HELPER METHODS ###############################################

        private async Task<ApplicationUser> getAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        private void removeOldRefreshTokens(ApplicationUser user)
        {
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_jwt.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private (RefreshToken, ApplicationUser) getOldRefreshToken(string token)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null) return (null, null);

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive) throw new AppException("Invalid token");

            return (refreshToken, user);
        }
    }
}

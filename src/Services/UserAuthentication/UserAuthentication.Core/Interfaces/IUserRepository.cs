using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eShopWithReact.Services.UserAuthentication.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace eShopWithReact.Services.UserAuthentication.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<RegisterResponse> Register(RegisterRequest registerUser);
        Task<bool> VerifyEmail(string email, string token);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest, string ipAddress);
        Task<string> AddRoleAsync(AddRole addRole);
        Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
        Task RevokeToken(string token, string ipAddress);
        void ValidateResetToken(ValidateResetTokenRequest model);
        Task<string> ForgotPassword(ForgotPasswordRequest model, string origin);
        Task ResetPassword(ResetPasswordRequest model);
        Task ChangePassword(ChangePasswordRequest model);
        Task<IEnumerable<AccountResponse>> GetAll();
        Task<AccountResponse> GetById(string id);
        Task<AccountResponse> Update(string id, UpdateRequest model);
        Task Delete(string id);
    }
}

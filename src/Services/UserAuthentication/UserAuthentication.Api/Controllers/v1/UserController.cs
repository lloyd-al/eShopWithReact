using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using eShopWithReact.Common.Core.Entities;
using eShopWithReact.Common.Core.Interfaces;
using eShopWithReact.Services.UserAuthentication.Core.Entities;
using eShopWithReact.Services.UserAuthentication.Core.Interfaces;
using eShopWithReact.Services.UserAuthentication.Core.Models;


namespace eShopWithReact.Services.UserAuthentication.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : RootController
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UserController(
            IUserRepository userRepository, 
            UserManager<ApplicationUser> userManager, 
            IMapper mapper, IEmailService emailService, 
            ILoggerManager logger) : base(logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerUser)
        {
            RegisterResponse response = await _userRepository.Register(registerUser);

            if (response.Success)
            {
                await sendVerificationEmail(response);
            }

            return Ok(new { message = response.Message });
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            await _userRepository.VerifyEmail(email, token);
            return Ok(new { message = "Verification successful, you can now login" });
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Missing login details");
            }

            AuthenticateResponse response = await _userRepository.Authenticate(request, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRole model)
        {
            var result = await _userRepository.AddRoleAsync(model);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponse>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            AuthenticateResponse response = await _userRepository.RefreshToken(refreshToken, ipAddress());
            if (!string.IsNullOrEmpty(response.RefreshToken))
                setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest request)
        {
            // accept token from request body or cookie
            var token = request.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            //// users can revoke their own tokens and admins can revoke any tokens
            //var isAdmin = await _userManager.IsInRoleAsync(applicationUser, UserRole.Admin);
            //if (!applicationUser.OwnsToken(token) && !isAdmin)
            //    return Unauthorized(new { message = "Unauthorized" });

            await _userRepository.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        [HttpPost("validate-reset-token")]
        public IActionResult ValidateResetToken(ValidateResetTokenRequest model)
        {
            _userRepository.ValidateResetToken(model);
            return Ok(new { message = "Token is valid" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            var origin = Request.Headers["origin"];
            var token = await _userRepository.ForgotPassword(model, origin);

            if (token != string.Empty)
                await sendPasswordResetEmail(model.Email, token, origin);

            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            await _userRepository.ResetPassword(model);

            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest model)
        {
            await _userRepository.ChangePassword(model);

            return Ok(new { message = "Password changed successfully, you can now login" });
        }

        //[Authorize(UserRole.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = _userRepository.GetAll();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountResponse>> GetById(string id)
        {
            // users can get their own account and admins can get any account
            //var isAdmin = await _userManager.IsInRoleAsync(applicationUser, UserRole.Admin);
            //if (id != applicationUser.Id && !isAdmin)
            //    return Unauthorized(new { message = "Unauthorized" });

            var account = await _userRepository.GetById(id);
            return Ok(account);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountResponse>> Update(string id, UpdateRequest model)
        {
            // users can update their own account and admins can update any account
            //var isAdmin = await _userManager.IsInRoleAsync(applicationUser, UserRole.Admin);
            //if (id != applicationUser.Id && !isAdmin)
            //    return Unauthorized(new { message = "Unauthorized" });

            // only admins can update role
            //if (!isAdmin)
            //    model.Role = null;

            var account = await _userRepository.Update(id, model);
            return Ok(account);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // users can delete their own account and admins can delete any account
            //var isAdmin = await _userManager.IsInRoleAsync(applicationUser, UserRole.Admin);
            //if (id != applicationUser.Id && !isAdmin)
            //    return Unauthorized(new { message = "Unauthorized" });

            await _userRepository.Delete(id);
            return Ok(new { message = "Account deleted successfully" });
        }

        // Helper methods

        private async Task sendVerificationEmail(RegisterResponse requestResponse)
        {
            var origin = Request.Headers["origin"];
            var verifyUrl = $"{origin}/user/verify-email?email={requestResponse.Email}&token={requestResponse.Token}";

            string templatePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(templatePath);
            string mailMessage = str.ReadToEnd();
            str.Close();

            mailMessage = mailMessage
                                .Replace("[firstname]", requestResponse.FirstName)
                                .Replace("[lastname]", requestResponse.LastName)
                                .Replace("[email]", requestResponse.Email)
                                .Replace("[verifyurl]", verifyUrl);

            string message;



            message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";

            EmailMessage email = new EmailMessage();
            email.To = requestResponse.Email;
            email.Subject = "Sign-up Verification - Verify Email";
            email.Content = $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}";
            await _emailService.SendEmailAsync(email);
        }

        private async Task sendPasswordResetEmail(string email, string token, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/user/reset-password?token={token}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/user/reset-password</code> api route:</p>
                             <p><code>{token}</code></p>";
            }

            EmailMessage emailMessage = new EmailMessage();
            emailMessage.To = email;
            emailMessage.Subject = "Sign-up Verification API - Reset Password";
            emailMessage.Content = $@"<h4>Reset Password Email</h4>
                         {message}";
            await _emailService.SendEmailAsync(emailMessage);
        }

        /// <summary>
        /// The helper method appends an HTTP Only cookie containing the refresh token to the response for increased security. 
        /// HTTP Only cookies are not accessible to client-side javascript which prevents XSS (cross site scripting), and 
        /// the refresh token can only be used to fetch a new token from the /accounts/refresh-token route which prevents CSRF (cross site request forgery).
        /// </summary>
        /// <param name="token"></param>
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}

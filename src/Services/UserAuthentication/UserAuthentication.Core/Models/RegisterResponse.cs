using System;


namespace eShopWithReact.Services.UserAuthentication.Core.Models
{
    public class RegisterResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}

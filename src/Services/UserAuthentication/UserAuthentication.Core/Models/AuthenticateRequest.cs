using System.ComponentModel.DataAnnotations;

namespace eShopWithReact.Services.UserAuthentication.Core.Models
{
    public class AuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

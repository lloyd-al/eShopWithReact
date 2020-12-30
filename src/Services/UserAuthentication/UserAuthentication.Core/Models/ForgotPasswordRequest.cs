using System.ComponentModel.DataAnnotations;

namespace eShopWithReact.Services.UserAuthentication.Core.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

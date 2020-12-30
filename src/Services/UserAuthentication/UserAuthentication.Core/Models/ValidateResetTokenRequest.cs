using System.ComponentModel.DataAnnotations;

namespace eShopWithReact.Services.UserAuthentication.Core.Models
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}

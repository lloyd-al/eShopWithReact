using System;
using System.ComponentModel.DataAnnotations;

namespace eShopWithReact.Services.UserAuthentication.Core.Models
{
    public class AddRole
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}

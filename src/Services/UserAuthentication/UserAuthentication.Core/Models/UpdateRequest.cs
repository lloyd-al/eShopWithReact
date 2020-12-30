using System.ComponentModel.DataAnnotations;
using eShopWithReact.Services.UserAuthentication.Core.Entities;

namespace eShopWithReact.Services.UserAuthentication.Core.Models
{
    public class UpdateRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

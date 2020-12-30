using System;
using System.Collections.Generic;

namespace eShopWithReact.Services.UserAuthentication.Core.Models
{
    /// <summary>
    /// The account response model defines the account data returned by the GetAll, GetById, Create and Update methods of the accounts controller 
    /// and account service. It includes basic account details and excludes sensitive data such as hashed passwords and tokens.
    /// </summary>
    public class AccountResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLockedOut { get; set; }
    }
}

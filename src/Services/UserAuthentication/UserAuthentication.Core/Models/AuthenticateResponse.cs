using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eShopWithReact.Services.UserAuthentication.Core.Models
{
    /// <summary>
    /// The authenticate response model defines the data returned by the Authenticate and RefreshToken methods of the accounts controller and 
    /// account service. It includes basic account details, a jwt token and a refresh token.
    /// The refresh token property is decorated with the [JsonIgnore] attribute which prevents the property from being returned in the 
    /// api response body. This is because the refresh token is returned as an HTTP Only cookie instead of in the body.
    /// </summary>
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}

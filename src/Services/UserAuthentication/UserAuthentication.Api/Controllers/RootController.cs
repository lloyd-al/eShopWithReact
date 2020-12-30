using System;
using Microsoft.AspNetCore.Mvc;
using eShopWithReact.Common.Core.Interfaces;
using eShopWithReact.Services.UserAuthentication.Core.Entities;

namespace eShopWithReact.Services.UserAuthentication.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RootController : ControllerBase
    {
        // returns the current authenticated account (null if not logged in)
        //public ApplicationUser applicationUser => (ApplicationUser)HttpContext.Items["ApplicationUser"];
        protected readonly ILoggerManager _logger;
        public RootController(ILoggerManager logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}

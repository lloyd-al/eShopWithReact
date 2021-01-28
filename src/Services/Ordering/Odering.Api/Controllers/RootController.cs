using System;
using eShopWithReact.Common.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace eShopWithReact.Services.Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RootController : ControllerBase
    {
        protected readonly ILoggerManager _logger;
        public RootController(ILoggerManager logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}

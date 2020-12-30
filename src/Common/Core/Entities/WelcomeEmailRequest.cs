using System;
using System.Collections.Generic;
using System.Text;

namespace eShopWithReact.Common.Core.Entities
{
    public class WelcomeEmailRequest
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
    }
}

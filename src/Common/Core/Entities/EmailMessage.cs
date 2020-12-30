using Microsoft.AspNetCore.Http;


namespace eShopWithReact.Common.Core.Entities
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }
    }
}

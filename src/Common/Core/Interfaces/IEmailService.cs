using System.Threading.Tasks;
using eShopWithReact.Common.Core.Entities;


namespace eShopWithReact.Common.Core.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage emailMessage);
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}

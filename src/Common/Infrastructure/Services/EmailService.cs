using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using eShopWithReact.Common.Core.Entities;
using eShopWithReact.Common.Core.Interfaces;
using eShopWithReact.Common.Infrastructure.Settings;


namespace eShopWithReact.Common.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public MailSetting _mailSettings { get; }
        public ILoggerManager _logger { get; }

        public EmailService(IOptions<MailSetting> mailSettings, ILoggerManager logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public void SendEmail(EmailMessage emailMessage)
        {
            var mailMessage = CreateEmailMessage(emailMessage);

            Send(mailMessage);
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            var mailMessage = CreateEmailMessage(emailMessage);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage emailMessage)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailMessage.From ?? _mailSettings.EmailFrom));
            email.To.Add(MailboxAddress.Parse(emailMessage.To));
            email.Subject = emailMessage.Subject;

            var builder = new BodyBuilder { HtmlBody = emailMessage.Content };

            if (emailMessage.Attachments != null && emailMessage.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var file in emailMessage.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            email.Body = builder.ToMessageBody(); ;

            return email;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPassword);
                    await client.SendAsync(mailMessage);

                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message);
                    //throw new ApiException(ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPassword);
                    client.Send(mailMessage);

                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message);
                    //throw new ApiException(ex.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}

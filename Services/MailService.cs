using DataAccess.Data.Interfaces;
using Eventor.Data.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Eventor.Data.Entities;

namespace Eventor.Services
{
    public class MailService : IMailService
    {
        private readonly IOptions<Models.MailConfig> options;
        private readonly IRepository<ApplicationUser> repository;

        public MailService(IOptions<Models.MailConfig> config, IRepository<ApplicationUser> repository)
        {
            this.options = config;
            this.repository = repository;
        }

        public void Send(string subject, string body)
        {
            using (var message = new MailMessage())
            {
                var users = repository.GetAll();

                foreach (var user in users)
                {
                    message.To.Add(new MailAddress(user.Email));
                }

                message.From = new MailAddress("blankeventor@gmail.com", "Eventor");

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient(options.Value.Host))
                {
                    client.Port = options.Value.Port;
                    client.Credentials = new NetworkCredential(options.Value.Login, options.Value.Password);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
        }
    }
}

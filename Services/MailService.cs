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
using DataAccess.Entities;
using Services.DTO;

namespace Eventor.Services
{
    public class MailService : IMailService
    {
        private readonly IOptions<Models.MailConfig> options;
        private readonly IRepository<Subscription> repository;

        public MailService(IOptions<Models.MailConfig> config, IRepository<Subscription> repository)
        {
            this.options = config;
            this.repository = repository;
        }

        public void Send(EmailDTO email, string userId)
        {
            using (var message = new MailMessage())
            {
                var subscriptions = repository.GetAll().Where(s => s.AreNotificationsEnabled && s.EventId == email.EventId && s.Event.OrganizerId == userId);

                foreach (var subscription in subscriptions)
                {
                    message.To.Add(new MailAddress(subscription.User.Email));
                }

                message.From = new MailAddress("blankeventor@gmail.com", "Eventor");

                message.Subject = email.Subject;
                message.Body = email.Body;                

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

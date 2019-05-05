using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Services
{
    public interface IMailService
    {
        void Send(EmailDTO email, string userId);
    }
}

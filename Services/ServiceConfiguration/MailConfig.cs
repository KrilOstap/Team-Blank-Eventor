using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Models
{
    public class MailConfig
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

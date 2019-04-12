using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventor.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IMailService service;

        public EmailController(IMailService service)
        {
            this.service = service;
        }

        public IActionResult SendEmail()
        {          
            return View();
        }

        [HttpPost]        
        public IActionResult SendEmail(string subject, string body)
        {
            service.Send(subject, body);
            return View();
        }
    }
}
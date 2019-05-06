using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Eventor.Models;
using Eventor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;

namespace Eventor.Controllers
{
    [Authorize(Roles = "Organizer")]
    public class EmailController : Controller
    {
        private readonly IMailService service;
        private readonly IMapper mapper;

        public EmailController(IMailService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public IActionResult EmailError()
        {
            return View();
        }

        public IActionResult SendEmail(string id)
        {          
            return View(new EmailModel { EventId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]       
        public IActionResult SendEmail([Bind("Subject", "Body", "EventId")] EmailModel email)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var emailToSend = mapper.Map<EmailModel, EmailDTO>(email);

            try
            {
                service.Send(emailToSend, userId);
            }
            catch (Exception e)
            {
                return View(nameof(EmailError));
            }

            return RedirectToAction("Index", "Event", new { Id = userId });
        }
    }
}
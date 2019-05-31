using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;

namespace Eventor.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService subscriptionService;    
        private readonly IMapper mapper;

        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            this.subscriptionService = subscriptionService;          
            this.mapper = mapper;
        }
      
        public IActionResult Subscribe(string id)
        {
            var subscription = new SubscriptionDTO
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                EventId = id,
                AreNotificationsEnabled = true
            };

            subscriptionService.Add(subscription);

            return RedirectToAction("Details", "Event", new { eventId  = id });
        }

        public IActionResult Unsubscribe(string id)
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;           
            subscriptionService.Delete(UserId, id);

            return RedirectToAction("Index", new { Id = UserId });
        }

        public IActionResult UnsubscribeFromEvent(string id)
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            subscriptionService.Delete(UserId, id);

            return RedirectToAction("Details", "Event", new { eventId = id } );
        }

        public IActionResult Index(string id)
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;         
            return View(subscriptionService.GetSubscriptionsForUser(UserId));
        }
    }
}
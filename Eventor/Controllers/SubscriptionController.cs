using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;

namespace Eventor.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public SubscriptionController(ISubscriptionService subscriptionService, IEventService eventService, IMapper mapper)
        {
            this.subscriptionService = subscriptionService;
            this.eventService = eventService;
            this.mapper = mapper;
        }
    
        public IActionResult Subscribe(string id)
        {
            var subscription = new SubscriptionDTO
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                EventId = eventService.GetById(id).Id
            };

            subscriptionService.Add(subscription);

            return RedirectToAction("Index", "Event");
        }

        public IActionResult Unsubscribe(string id)
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;           
            subscriptionService.Delete(UserId, id);

            return RedirectToAction("Index", "Event");
        }

        public IActionResult Index(string id)
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;         
            return View(subscriptionService.GetSubscriptionsForUser(UserId));
        }
    }
}
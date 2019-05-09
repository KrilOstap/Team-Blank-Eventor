using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Data.Entities;
using Eventor.Models;
using Eventor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Eventor.Data.Entities;
using System.Security.Claims;

namespace Eventor.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IImageService imageService;
        private readonly ISubscriptionService subscriptionService;       
        private readonly IMapper mapper;

        public EventController(IEventService eventService, IImageService imageService,
            ISubscriptionService subscriptionService,
            UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.eventService = eventService;
            this.imageService = imageService;
            this.subscriptionService = subscriptionService;            
            this.mapper = mapper;
        }

        public IActionResult Index()
        {                    
            return View(eventService.GetFutureEvents());
        }

        [Authorize(Roles = "Organizer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organizer")]
        public IActionResult Create([Bind("Date", "Title", "Description", "City", "Address", "Number")]
        EventDTO @event, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length != 0)
                {                    
                    string path = imageService.Save(file);
                    @event.ImagePath = path;
                    @event.OrganizerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    eventService.Add(@event);
                    return RedirectToAction(nameof(Create));
                }
            }

            return View(@event);
        }

        [Authorize(Roles = "Organizer")]
        public IActionResult Remove(string id)
        {
            EventDTO @event = eventService.GetById(id);
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (@event == null)
            {
                return NotFound();
            }

            if (currentUserId == @event.OrganizerId)
            {
                subscriptionService.UnsubscribeAll(id);
                eventService.Delete(id);
                imageService.Delete(@event.ImagePath);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Organizer")]
        public IActionResult Edit(string id)
        {
            EventDTO @event = eventService.GetById(id);
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organizer")]
        public IActionResult Edit([Bind("Id, Date", "Title", "Description", "City",
        "Address", "Number, ImagePath")]EventDTO @event)
        {
            if (ModelState.IsValid)
            {
                eventService.Update(@event);
                return RedirectToAction(nameof(Index));
            }

            return View(@event);
        }

        public IActionResult Details(string eventId)
        {
            EventDTO @event = eventService.GetById(eventId);
            EventDetailsModel eventModel = mapper.Map<EventDTO, EventDetailsModel>(@event);

            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            eventModel.NumberOfSubscribers = subscriptionService.GetNumberOfSubscribers(eventId);

            if (eventModel.OrganizerId == currentUserId)
            {
               eventModel.IsOwner = true;
            }

            if (subscriptionService.IsSubscribed(currentUserId, eventId))
            {
               eventModel.IsSubscribed = true;
            }
                 
            return View(eventModel);
        }        
    }
}
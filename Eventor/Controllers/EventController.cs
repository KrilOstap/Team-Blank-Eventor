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
using X.PagedList;

namespace Eventor.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IImageService imageService;
        private readonly ISubscriptionService subscriptionService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public EventController(IEventService eventService, IImageService imageService,
            ISubscriptionService subscriptionService,
            UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.eventService = eventService;
            this.imageService = imageService;
            this.subscriptionService = subscriptionService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IActionResult Index(int? pageNumber)
        {
            var events = eventService.GetEvents();
            var currentPage = pageNumber ?? 1;
            var onePageOfEvents = events.ToPagedList(currentPage, 4);
            return View(onePageOfEvents);
        }

        public IActionResult Create()
        {
            return View();
        }

       [HttpPost]
        //[Authorize(Roles = "Organaizer")]
        public IActionResult Create([Bind("Date", "Title", "Description", "City", "Address", "Number")]
        EventDTO @event, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file.Length != 0 && file != null)
                {                    
                    string path = imageService.Save(file);
                    @event.ImagePath = path;
                    @event.OrganizerId = userManager.GetUserId(HttpContext.User);
                    eventService.Add(@event);
                }
            }
                     
            return View();
        }

        [Authorize(Roles = "Organaizer")]
        public IActionResult Remove(string id)
        {
            EventDTO @event = eventService.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }

            return View();
        }

        [Authorize(Roles = "Organaizer")]
        public IActionResult Edit(string id)
        {
            EventDTO @event = eventService.GetById(id);
            return View(@event);
        }

        public IActionResult Details(string id)
        {
            EventDTO @event = eventService.GetById(id);
            EventDetailsModel eventModel = mapper.Map<EventDTO, EventDetailsModel>(@event);
            string currentUserId = userManager.GetUserId(HttpContext.User);

            if (eventModel.OrganizerId == currentUserId)
            {
                eventModel.IsOwner = true;
            }

            if (subscriptionService.IsSubscribed(currentUserId, id))
            {
                eventModel.IsSubscribed = true;
            }

            return View(eventModel);
        }        
    }
}
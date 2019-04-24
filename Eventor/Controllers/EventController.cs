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
using PagedList.Core;
using Microsoft.AspNetCore.Identity;
using Eventor.Data.Entities;

namespace Eventor.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IImageService imageService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public EventController(IEventService service, IMapper mapper,
            IImageService imageService, UserManager<ApplicationUser> userManager)
        {
            this.eventService = service;
            this.mapper = mapper;
            this.imageService = imageService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {           
            return View(eventService.GetEvents());
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
            return View(@event);
        }        
    }
}
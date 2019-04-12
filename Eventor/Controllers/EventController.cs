using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Data.Entities;
using Eventor.Models;
using Eventor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;

namespace Eventor.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService service;
        private readonly IMapper mapper;

        public EventController(IEventService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public IActionResult Events()
        {           
            return View(service.GetEvents().Select(item => mapper.Map<EventDTO, EventModel>(item)));
        }

        [Authorize(Roles = "Organaizer")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Organaizer")]
        public IActionResult Remove(int id)
        {
            EventDTO @event = service.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult Description(int id)
        {
            EventDTO @event = service.GetById(id);
            return View(mapper.Map<EventDTO, EventModel>(@event));
        }

        public IActionResult Edit(int id)
        {
            EventDTO @event = service.GetById(id);
            return View(mapper.Map<EventDTO, EventModel>(@event));
        }

        public IActionResult Subscribe(int id)
        {
            EventDTO @event = service.GetById(id);
            return View();
        }
    }
}
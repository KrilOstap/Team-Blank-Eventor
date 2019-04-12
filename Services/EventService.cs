using AutoMapper;
using DataAccess.Data.Entities;
using DataAccess.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> repository;
        private readonly IMapper mapper;

        public EventService(IRepository<Event> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<EventDTO> GetEvents()
        {
            return repository.GetAll().Select(item => mapper.Map<Event, EventDTO>(item));
        }

        public EventDTO GetById(int id)
        {
            return mapper.Map<Event, EventDTO>(repository.GetById(id));
        }

        public void Delete(int id)
        {
            Event @event = repository.GetById(id);

            if (@event.ImagePath != null && @event.ImagePath.Length != 0)
            {
                File.Delete(@event.ImagePath);
            }

            repository.DeleteById(id);
        }

        public void Create(EventDTO eventDTO, IFormFile file)
        {
            Event @event = mapper.Map<EventDTO, Event>(eventDTO);

            if (file != null && file.Length > 0)
            {
               // To Do image saving logic
            }

            repository.Add(@event);
            repository.Save();
        }
    }
}

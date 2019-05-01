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

        public EventDTO GetById(string id)
        {
            return mapper.Map<Event, EventDTO>(repository.GetById(id));
        }

        public void Delete(string id)
        {
            Event @event = repository.GetById(id);

            repository.DeleteById(id);
            repository.Save();
        }

        public void Add(EventDTO eventDTO)
        {
            Event @event = mapper.Map<EventDTO, Event>(eventDTO);          
            repository.Add(@event);
            repository.Save();
        }

        public void Update(EventDTO eventDTO)
        {
            Event @event = mapper.Map<EventDTO, Event>(eventDTO);
            repository.Update(@event);
            repository.Save();
        }
    }
}

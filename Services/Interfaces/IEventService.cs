using Microsoft.AspNetCore.Http;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IEventService
    {
        IEnumerable<EventDTO> GetEvents();
        EventDTO GetById(string id);
        void Delete(string id);
        void Add(EventDTO eventDTO);       
    }
}

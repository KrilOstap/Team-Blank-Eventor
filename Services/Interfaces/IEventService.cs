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
        EventDTO GetById(int id);
        void Delete(int id);
        void Create(EventDTO eventDTO, IFormFile file);       
    }
}

using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface ISubscriptionService
    {
        IEnumerable<SubscriptionDTO> GetEvents(string id);
        SubscriptionDTO GetById(string id);
        void Delete(string id);
        void Add(SubscriptionDTO eventDTO);
    }
}

using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface ISubscriptionService
    {
        IEnumerable<SubscriptionDTO> GetSubscriptions(string id);
        SubscriptionDTO GetById(string id);
        void Delete(string userId, string eventId);
        void Add(SubscriptionDTO eventDTO);
        bool IsSubscribed(string userId, string eventId);
    }
}

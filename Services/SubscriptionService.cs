using AutoMapper;
using DataAccess.Data.Interfaces;
using DataAccess.Entities;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<Subscription> repository;
        private readonly IMapper mapper;

        public SubscriptionService(IRepository<Subscription> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void Add(SubscriptionDTO item)
        {
            repository.Add(mapper.Map<SubscriptionDTO, Subscription>(item));
            repository.Save();
        }

        public void Delete(string id)
        {
            repository.DeleteById(id);
            repository.Save();
        }

        public SubscriptionDTO GetById(string id)
        {       
            return mapper.Map<Subscription, SubscriptionDTO>(repository.GetById(id));
        }

        public IEnumerable<SubscriptionDTO> GetSubscriptions(string id)
        {
           var subscriptions = repository.GetAll().Where(s => s.OrganizerId == id);
            return mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionDTO>>(subscriptions);
        }
    
    }
}

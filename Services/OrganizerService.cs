using AutoMapper;
using DataAccess.Data.Entities;
using DataAccess.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IRepository<Organizer> repository;
        private readonly IMapper mapper;

        public OrganizerService(IRepository<Organizer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<OrganizerDTO> GetOrganizers()
        {
            return repository.GetAll().Select(item => mapper.Map<Organizer, OrganizerDTO>(item));
        }

        public OrganizerDTO GetById(int id)
        {
            return mapper.Map<Organizer, OrganizerDTO>(repository.GetById(id));
        }

        public void Delete(int id)
        {
            Organizer organizer = repository.GetById(id);
        
            repository.DeleteById(id);
        }

        public void Create(OrganizerDTO organizerDTO)
        {
            Organizer organizer = mapper.Map<OrganizerDTO, Organizer>(organizerDTO);
           
            repository.Add(organizer);
            repository.Save();
        }
    }
}

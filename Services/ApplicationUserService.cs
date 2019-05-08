using AutoMapper;
using DataAccess.Data.Interfaces;
using Eventor.Data.Entities;
using Eventor.Data.Repositories;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IRepository<ApplicationUser> repository;
        private readonly IMapper mapper;

        public ApplicationUserService(IRepository<ApplicationUser> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public ApplicationUserDTO GetById(string id)
        {
            return mapper.Map<ApplicationUser, ApplicationUserDTO>(repository.GetById(id));
        }

        public void UpdateInformation(ApplicationUser user)
        {
            repository.Update(user);
        }
    }
}

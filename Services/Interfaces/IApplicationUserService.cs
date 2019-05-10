using Eventor.Data.Entities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IApplicationUserService
    {
        void UpdateInformation(ApplicationUser user);
        ApplicationUserDTO GetById(string id);
    }
}

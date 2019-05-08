﻿using Eventor.Data.Entities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IApplicationUserService
    {
        ApplicationUserDTO GetById(string id);
        void UpdateInformation(ApplicationUser user);
    }
}

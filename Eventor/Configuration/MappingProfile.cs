﻿using AutoMapper;
using DataAccess.Data.Entities;
using Eventor.Models;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {            
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();
            CreateMap<Organizer, OrganizerDTO>();
            CreateMap<OrganizerDTO, Organizer>();
            CreateMap<OrganizerDTO, OrganizerModel>();
        }
    }
}
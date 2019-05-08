using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Eventor.Data.Entities;
using Eventor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;

namespace Eventor.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUserService userService;
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public UserController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IApplicationUserService userService,
            IEventService eventService, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userService = userService;
            this.eventService = eventService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Profile(string organizerId)
        {
            var user = userService.GetById(organizerId);
            var profile = mapper.Map<ApplicationUserDTO, ProfileModel>(user);

            if (User.IsInRole("Organizer") && 
                User.FindFirst(ClaimTypes.NameIdentifier).Value == organizerId)
            {
                profile.IsOwner = true;
            }
         
            profile.Events = eventService.GetAllEvents(organizerId);

            return View(profile);
        }

        public IActionResult SuccessfulPromotion()
        {
            return View();
        }

        public async Task<IActionResult> PromoteToOrganizer()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = await userManager.FindByIdAsync(userId);

            if (user != null && !User.IsInRole("Organizer"))
            {
                var result = await userManager.AddToRoleAsync(user, "Organizer");
            }

            return View(nameof(SuccessfulPromotion));
        }
    }
}
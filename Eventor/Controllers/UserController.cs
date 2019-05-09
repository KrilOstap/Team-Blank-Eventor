﻿using System;
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
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IApplicationUserService userService;
        private readonly IEventService eventService;        
        private readonly IMapper mapper;

        public UserController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IApplicationUserService userService,
            IEventService eventService, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userService = userService;
            this.eventService = eventService;
            this.mapper = mapper;
            this.signInManager = signInManager;
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

        [Authorize]
        public IActionResult SuccessfulPromotion()
        {
            return View();
        }

        [Authorize]
        public IActionResult PromoteToOrganizer()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteToOrganizer([Bind("FirstName",
            "LastName", "Password")] PromotionModel promotion)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ApplicationUser user = await userManager.FindByIdAsync(userId);
                var login = await signInManager.CheckPasswordSignInAsync(user, promotion.Password, false);
                
                if (user != null && !User.IsInRole("Organizer") && login.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, "Organizer");
                    user.FirstName = promotion.FirstName;
                    user.LastName = promotion.LastName;
                    userService.UpdateInformation(user);
                    return View(nameof(SuccessfulPromotion));
                }               
            }

            promotion.LogginFailed = true;
            return View(promotion);
        }
    }
}
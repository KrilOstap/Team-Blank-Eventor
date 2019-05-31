using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Eventor.Data.Entities;
using Eventor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;

namespace Eventor.Controllers
{    
    public class UserController : Controller
    {        
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IApplicationUserService userService;
        private readonly IEventService eventService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IApplicationUserService userService, IEventService eventService, IImageService imageService, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
            this.eventService = eventService;
            this.imageService = imageService;
            this.mapper = mapper;
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
        public  async Task<IActionResult> PromoteToOrganizer()
        {
            ApplicationUser user = await userManager
                .FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user.AppliedForPromotion)
            {
                return RedirectToAction("PromotionRequested");
            }

            return View();
        }

        [Authorize]
        public IActionResult PromotionRequested()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageUsers()
        {
            return View(userService.GetAll().Where(u => u.AppliedForPromotion));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteToOrganizer([Bind("FirstName",
            "LastName", "Password")] PromotionModel promotion, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    var login = await signInManager.CheckPasswordSignInAsync(user, promotion.Password, false);

                    if (user != null && !User.IsInRole("Organizer") && login.Succeeded)
                    {
                        user.AppliedForPromotion = true;
                        user.FirstName = promotion.FirstName;
                        user.LastName = promotion.LastName;
                        user.ImagePath = imageService.Save(file);
                        userService.UpdateInformation(user);
                        return View(nameof(PromotionRequested));
                    }
                }                       
            }



            promotion.LogginFailed = true;
            return View(promotion);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PromotioConfirmed(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            var result = await userManager.AddToRoleAsync(user, "Organizer");
            user.AppliedForPromotion = false;
            userService.UpdateInformation(user);
            return RedirectToAction("ManageUsers");
        }
    }
}
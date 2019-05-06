using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eventor.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eventor.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
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

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult SuccessfulPromotion()
        {
            return View();
        }
    }
}
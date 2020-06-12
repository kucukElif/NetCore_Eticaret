using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AppUserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppUser> roleManager;

        public AppUserController(UserManager<AppUser> userManager, RoleManager<AppUser> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public async Task<IActionResult> AssignMember(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            await userManager.AddToRoleAsync(user, "Member");
            return View();
        }

        public async Task<IActionResult> AssignAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            await userManager.AddToRoleAsync(user, "Admin");
            return View();
        }
    }
}

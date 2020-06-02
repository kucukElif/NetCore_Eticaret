﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.ViewModels;

namespace MVC.Controllers
{
    public class MemberController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public MemberController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Create(AppUserVM appUserVM) 
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = appUserVM.UserName,
                    FirstName = appUserVM.FirstName,
                    LastName = appUserVM.LastName,
                    Email = appUserVM.Email,
                    Gender = appUserVM.Gender,
                    City = appUserVM.City
                };
              var result = await  userManager.CreateAsync(user, appUserVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");

                }
            }
            return View();
        }
    }
}

﻿using Company.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser>userManager,ILogger<UserController> logger)
        {
           _userManager = userManager;
           _logger = logger;
        }
        public async Task <IActionResult> Index(string searchInp)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(searchInp))
            {
                users=await _userManager.Users.ToListAsync();
                return View(users);
            }

        
            else
            {
                 users =await _userManager.Users.Where(user => user.NormalizedEmail.Trim().Contains(searchInp.Trim().ToUpper())).ToListAsync();
                return View(users);
            }
        }
        [HttpGet]
        public async Task <IActionResult> Details(string? id, string viewname = "Details")
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return View(viewname, user);
        }

    }
}

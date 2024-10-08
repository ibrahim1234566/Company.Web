using Company.Data.Models;
using Company.Web.Models;
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
            if (viewname == "Update")
            {

                var UserModel = new UserUpdateViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
                return View(viewname,UserModel);
            }
            return View(viewname, user);
        }

          [HttpGet]
    public async Task <IActionResult> Update(string? id)
    {
        return await Details(id, "Update");

    }
    [HttpPost]
    public async Task<IActionResult> Update(string? id,UserUpdateViewModel appUserUpdate)
    {
            if (id != appUserUpdate.Id) 
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null)
                    {
                        return NotFound();
                    }
                    user.UserName = appUserUpdate.UserName;
                    user.NormalizedUserName = appUserUpdate.UserName.ToUpper();
                    var res= await _userManager.UpdateAsync(user);
                    if (res.Succeeded)
                    {
                        _logger.LogInformation("user updated successfully");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _logger.LogInformation("user updated Failed");
                        return View(appUserUpdate);

                    }


                }
                catch (Exception ex)
                {
                   _logger.LogInformation(ex.Message);

                }
            
            }

        return View(appUserUpdate);
    }


    }
}

using Company.Data.Models;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize (Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RolesController> _logger;

        public RolesController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var Roles = await _roleManager.Roles.ToListAsync();
            return View(Roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = roleViewModel.Name

                };
                var res = await _roleManager.CreateAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index");

                }
                foreach (var i in res.Errors)
                {
                    _logger.LogInformation(i.Description);
                }
            }
            return View(roleViewModel);
        }
        public async Task<IActionResult> Details(string? id, string viewname = "Details")
        {
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }
            var roleviewmodel = new RoleViewModel
            {
                Id= Role.Id,
                Name= Role.Name
            };
            return View(viewname,roleviewmodel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");

        }
        [HttpPost]
        public async Task<IActionResult> Update(string? id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role is null)
                    {
                        return NotFound();
                    }
                    role.Name = roleViewModel.Name;
                    role.NormalizedName = roleViewModel.Name.ToUpper();
                    var res = await _roleManager.UpdateAsync(role);
                    if (res.Succeeded)
                    {
                        _logger.LogInformation("user updated successfully");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _logger.LogInformation("user updated Failed");
                        return View(roleViewModel);

                    }


                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);

                }

            }

            return View(roleViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                {
                    return NotFound();
                }
                var res = await _roleManager.DeleteAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var i in res.Errors)
                {
                    _logger.LogError(i.Description);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }


            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return NotFound();
            }
            ViewBag.RoleId = roleId;
            var users = await _userManager.Users.ToListAsync();
            var UserInRole = new List<UserInRoleViewModel>();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName=user.UserName

                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected=true;

                }
                else
                {
                    userInRole.IsSelected=false;
                }
                UserInRole.Add(userInRole);
            }
            return View(UserInRole);

        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UserInRoleViewModel>users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) 
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);
                    if (appuser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appuser, role.Name)) 
                        {
                            await _userManager.AddToRoleAsync(appuser, role.Name);
                        }
                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appuser, role.Name);

                        }

                    }

                }
                return RedirectToAction("Update", new { id = roleId });
            }
            return View(users);

        }
            

    }
}

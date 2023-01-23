using AspNetCoreIdentityApp.Web.Areas.Admin.Models;
using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin,RoleAction")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return View(roles);
        }
        [Authorize(Roles = "Admin,RoleAction")]
        public IActionResult RoleCreate()
        {
            return View();
        }
        [Authorize(Roles = "Admin,RoleAction")]
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole()
            {
                Name = request.Name

            });
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }

            TempData["SuccessMessage"] = "Rol eklenmiştir.";
            return RedirectToAction(nameof(RolesController.Index));

        }
        [Authorize(Roles = "Admin,RoleAction")]
        public async Task<IActionResult> RoleUpdate(string id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id);

            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek Rol Bulunamadı.");
            }
            return View(new RoleUpdateViewModel() { Id = roleToUpdate.Id, Name = roleToUpdate.Name });
        }
        [Authorize(Roles = "Admin,RoleAction")]
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id);

            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek Rol Bulunamadı.");
            }

            roleToUpdate.Name = request.Name;

            await _roleManager.UpdateAsync(roleToUpdate);

            ViewData["SuccessMessage"] = "Rol bilgisi güncellenmiştir.";

            return View();
        }
        [Authorize(Roles = "Admin,RoleAction")]
        public async Task<IActionResult> RoleDelete(string id)
        {
            var deleteToRole = await _roleManager.FindByIdAsync(id);
            if (deleteToRole == null)
            {
                throw new Exception("Silinecek Rol Bulunamadı.");
            }

            var result = await _roleManager.DeleteAsync(deleteToRole);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());
            }

            TempData["SuccessMessage"] = "Rol silinmiştir.";

            return RedirectToAction(nameof(RolesController.Index));
        }

        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            var currentUser = (await _userManager.FindByIdAsync(id))!;
            ViewBag.userId = id;
            var roles = await _roleManager.Roles.ToListAsync();
            var roleViewModelList = new List<AssignRoleToUserViewModel>();
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            foreach (var role in roles)
            {
                var assignRoleToUserViewModel = new AssignRoleToUserViewModel() { Id = role.Id, Name = role.Name };

                if (userRoles.Contains(role.Name!))
                {
                    assignRoleToUserViewModel.Exist = true;
                }

                roleViewModelList.Add(assignRoleToUserViewModel);
            }

            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, List<AssignRoleToUserViewModel> requestList)
        {
            var userToAssign = (await _userManager.FindByIdAsync(userId))!;

            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToAssign, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssign, role.Name);
                }
            }

            return RedirectToAction(nameof(HomeController.UserList), "Home");
        }

    }
}

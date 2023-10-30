using Identity_Application_Assignment.Models;
using Identity_Application_Assignment.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application_Assignment.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        //geting users and their roles
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList(); // Retrieve all users

            // Create a list to hold user information including roles
            var userViewModels = new List<UsersViewModel>();

            foreach (var user in users)
            {
                var userRoles = _userManager.GetRolesAsync(user).Result; 

                var userViewModel = new UsersViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Roles = userRoles
                };

                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }

        //endpoint to edit user
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // You may need to fetch available roles and add them to the view model
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Roles = roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userRoles.Contains(r.Name)
                }).ToList()
            };

            return View(model);
        }

        //edit post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Name = model.Name;
                user.Email = model.Email;

                // Update user roles based on the selected roles in the list
                var userRoles = await _userManager.GetRolesAsync(user);

                // Remove user from unselected roles
                var rolesToRemove = userRoles.Except(model.RoleName);
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                // Add user to selected roles
                var rolesToAdd = model.RoleName.Except(userRoles);
                await _userManager.AddToRolesAsync(user, rolesToAdd);

                // Update user properties
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If the ModelState is not valid, return to the edit view with the current model
            return View(model);
        }

        // Delete action to display the confirmation view
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Check if the user is in any roles and remove them
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles != null && userRoles.Any())
            {
                foreach (var role in userRoles)
                {
                    var results = await _userManager.RemoveFromRoleAsync(user, role);
                    if (!results.Succeeded)
                    {
                        ModelState.AddModelError("", "Failed to remove user from role.");
                        return View(user);
                    }
                }
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(user);
        }

    }
}

using Identity_Application_Assignment.Models;
using Identity_Application_Assignment.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Application_Assignment.Controllers
{
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _role;
        UserManager<ApplicationUser> _user;

        public RoleController(RoleManager<IdentityRole> role, UserManager<ApplicationUser> user)
        {
            _role = role;
            _user = user;

        }

        public IActionResult Index()
        {
            var roles = _role.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }


        //endpoint to create role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _role.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }

        //endpoint for edit role
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _role.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        //edit post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityRole role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingRole = await _role.FindByIdAsync(id);
                if (existingRole == null)
                {
                    return NotFound();
                }

                existingRole.Name = role.Name;
                var result = await _role.UpdateAsync(existingRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }

        //endpoint for delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _role.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _role.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            // Check if the role is assigned to any users
            var usersWithRole = await _user.GetUsersInRoleAsync(role.Name);

            if (usersWithRole.Count > 0)
            {
                TempData["ErrorMessage"] = "Cannot delete role because it is assigned to one or more users.";
            }
            else
            {
                var result = await _role.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("Delete", new { id });
        }

    }
}

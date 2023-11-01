using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Identity_Application_Assignment.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The passowrd and confirmation password do not match.")]
        public string ComfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public List<string> RoleName { get; set; }

        public List<SelectListItem>? Roles { get; set; }
    }
}

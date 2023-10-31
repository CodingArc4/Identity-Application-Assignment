using System.ComponentModel.DataAnnotations;

namespace Identity_Application_Assignment.ViewModels
{
    public class ForgetPasswordVM
    { 
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
    }
}

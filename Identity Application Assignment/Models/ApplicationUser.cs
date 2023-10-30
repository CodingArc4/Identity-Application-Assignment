using Microsoft.AspNetCore.Identity;

namespace Identity_Application_Assignment.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}

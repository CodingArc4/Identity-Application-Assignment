using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity_Application_Assignment.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> RoleName { get; set; }
        public List<SelectListItem>? Roles { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity_Application_Assignment.ViewModels
{
    public class RolesViewModel
    {
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}

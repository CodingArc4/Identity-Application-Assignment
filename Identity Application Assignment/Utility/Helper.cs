using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity_Application_Assignment.Utility
{
    public class Helper
    {
        public static string Admin = "Admin";
        public static string User = "User";

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value = Helper.Admin,Text=Helper.Admin}
                };
            }
            else
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value = Helper.Admin,Text=Helper.Admin},
                    new SelectListItem{Value = Helper.User,Text=Helper.User}
                };
            }

        }
    }
}

namespace Identity_Application_Assignment.ViewModels
{
 
        public class UsersViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public IList<string> Roles { get; set; }
        }

    
}

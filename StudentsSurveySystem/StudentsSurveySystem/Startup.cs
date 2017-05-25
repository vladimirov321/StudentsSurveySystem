using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using StudentsSurveySystem.Models;

[assembly: OwinStartupAttribute(typeof(StudentsSurveySystem.Startup))]
namespace StudentsSurveySystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }

        // In this method we will create an Admin user for login
        public void createRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User   
            if (!roleManager.RoleExists("Admin"))
            {
                //Create Admin Role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user 
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@system.com";

                string userPassword = "Admin321!";

                var chkUser = UserManager.Create(user, userPassword);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}

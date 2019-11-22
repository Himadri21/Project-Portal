using Microsoft.Owin;
using Owin;
using ProjectProgress.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(ProjectProgress.Startup))]
namespace ProjectProgress
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin@rnsit.com";
                user.Email = "admin@rnsit.com";
                string password = "P@assw0rd";

                var user1 = userManager.Create(user, password);
                if(user1.Succeeded)
                {
                    var result1=userManager.AddToRole(user.Id,"Admin");
                }
            }
            if(!roleManager.RoleExists("Teacher"))
            {
                var role = new IdentityRole();
                role.Name = "Teacher";
                roleManager.Create(role);
            }
        }
    }
}

using System;
using System.Data.Entity;
using Generic.Core.Logging;
using Generic.Infrastructure.Identity;
using Generic.Infrastructure.Repositories;
using Microsoft.AspNet.Identity;

namespace Generic.Infrastructure.Identity
{
    public class InitiateAdmin
    {
        private MyContext context;
        public InitiateAdmin(MyContext _context) {
            this.context = _context;
            InitializeIdentity();
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public void InitializeIdentity()
        {
            // This is only for testing purpose
            const string name = "stluong@admin.com";
            const string password = "TnTnT@2320";
            const string roleName = "Admin";
            var applicationRoleManager = IdentityFactory.CreateRoleManager(this.context);
            var applicationUserManager = IdentityFactory.CreateUserManager(this.context);
            //Create Role Admin if it does not exist
            var role = applicationRoleManager.FindByName(roleName);
            if (role == null)
            {
                role = new ApplicationIdentityRole { Name = roleName };
                applicationRoleManager.Create(role);
            }

            var user = applicationUserManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationIdentityUser { UserName = name, Email = name };
                applicationUserManager.Create(user, password);
                applicationUserManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = applicationUserManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                applicationUserManager.AddToRole(user.Id, role.Name);
            }

            this.context.SaveChanges();
        }
        
    }
}

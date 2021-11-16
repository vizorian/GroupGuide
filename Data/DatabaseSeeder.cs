using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<GroupGuideUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(UserManager<GroupGuideUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            // Roles
            foreach (var role in GroupGuideUserRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if(!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Admin user
            var newAdminUser = new GroupGuideUser()
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if(existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "Administrator123!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, GroupGuideUserRoles.All);
                }
            }
        }
    }
}

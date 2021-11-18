using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Repositories;
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
        private readonly IGamesRepository _gamesRepository;
        private readonly ICampaignsRepository _campaignsRepository;

        public DatabaseSeeder(UserManager<GroupGuideUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IGamesRepository gamesRepository,
                              ICampaignsRepository campaignsRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _gamesRepository = gamesRepository;
            _campaignsRepository = campaignsRepository;
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
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "Password123!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, GroupGuideUserRoles.All);
                }
            }

            // User 1
            var newUser1 = new GroupGuideUser()
            {
                UserName = "user1",
                Email = "user1@user1.com"
            };

            var existingUser1 = await _userManager.FindByNameAsync(newUser1.UserName);
            if(existingUser1 == null)
            {
                var createUser1Result = await _userManager.CreateAsync(newUser1, "Password123!");
                if (createUser1Result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser1, GroupGuideUserRoles.User);
                }
            }

            // User 2
            var newUser2 = new GroupGuideUser()
            {
                UserName = "user2",
                Email = "user2@user2.com"
            };

            var existingUser2 = await _userManager.FindByNameAsync(newUser2.UserName);
            if (existingUser2 == null)
            {
                var createUser2Result = await _userManager.CreateAsync(newUser2, "Password123!");
                if (createUser2Result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser2, GroupGuideUserRoles.User);
                }
            }

            // Game
            var newGame = new Game()
            {
                Name = "Dungeons & Dragons 5th edition",
                Description = "The core of D&D is storytelling. You and your friends tell a story together, " +
                "guiding your heroes through quests for treasure, battles with deadly foes, daring rescues, " +
                "courtly intrigue, and much more. You can also explore the world of Dungeons & Dragons through " +
                "any of the novels written by its fantasy authors, as well as engaging board games and immersive " +
                "video games. All of these stories are part of D&D."
            };
            var games = await _gamesRepository.GetAllAsync();
            bool gameExists = false;
            foreach (var game in games)
            {
                if(game.Name == newGame.Name)
                {
                    gameExists = true;
                    break;
                }
            }
            if (!gameExists)
                await _gamesRepository.CreateAsync(newGame);
        }
    }
}

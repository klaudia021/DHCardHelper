using DHCardHelper.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace DHCardHelper.Auth
{
    public static class SeedAuthentication
    {
        public async static Task SeedRolesAsync(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { RoleNames.Admin, RoleNames.GameMaster, RoleNames.Player };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public async static Task CreateAdminIfNotExist(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var email = "admin@admin.com";
            var password = "Admin123*";
            var name = "Admin";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = name
                };

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, RoleNames.Admin);
            }
        }

        public async static Task CreateUserIfNotExist(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var email = "player@player.com";
            var password = "Admin123*";
            var name = "Player";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = name
                };

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, RoleNames.Player);
            }
        }

        public async static Task CreateGameMasterIfNotExist(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var email = "gamemaster@gamemaster.com";
            var password = "Admin123*";
            var name = "Game Master";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = name
                };

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, RoleNames.GameMaster);
            }
        }

    }
}

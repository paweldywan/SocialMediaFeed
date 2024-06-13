using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaFeed.DAL
{
    public class SocialMediaFeedSeeder(SocialMediaFeedContext context, UserManager<IdentityUser> userManager)
    {
        public async Task Seed()
        {
            await Migrate();

            await SeedUsers();
        }

        private async Task Migrate()
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

            var canMigrate = pendingMigrations.Any();

            if (canMigrate)
                await context.Database.MigrateAsync();
        }

        private async Task CreateUser(string userName, string password)
        {
            if (userManager == null)
                return;

            var userExists = await userManager.Users.AnyAsync(u => u.UserName == userName);

            if (!userExists)
            {
                var user = new IdentityUser(userName)
                {
                    Email = userName
                };

                var result = await userManager.CreateAsync(user, password);

                if (result != IdentityResult.Success)
                    throw new InvalidOperationException("Could not create user in Seeding");
            }
        }

        private Task SeedUsers() => CreateUser("paweldywan@paweldywan.com", "P@ssw0rd");
    }
}

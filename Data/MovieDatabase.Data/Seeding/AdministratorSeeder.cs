namespace MovieDatabase.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MovieDatabase.Common;

    public class AdministratorSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.UserRoles.Any())
            {
                var roles = dbContext.UserRoles.ToList();

                var administratorExists = roles.Any(x => x.ToString().Contains(GlobalConstants.AdministratorRoleName));

                if (administratorExists == false)
                {
                    var user = dbContext.Users.FirstOrDefault();
                    var roleId = dbContext.Roles.FirstOrDefault(x => x.Name == GlobalConstants.AdministratorRoleName).Id;

                    if (user != null)
                    {
                        var userId = user.Id;
                        user.Roles.Add(new IdentityUserRole<string> { RoleId = roleId, UserId = userId });
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}

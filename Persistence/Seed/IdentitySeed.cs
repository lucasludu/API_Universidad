using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Seed
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync (IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = ["Admin", "Docente", "Estudiante"];

            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync (role))
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }

            var adminEmail = "admin@universidad.com";
            var name = "Admin";
            var lastName = "Universidad";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new ApplicationUser { UserName = adminEmail, Nombre = name, Apellido = lastName, Email = adminEmail };
                await userManager.CreateAsync(user, "Admin123!");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}

using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Seed
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync (IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Obtenemos la configuración
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            string[] roles = ["Admin", "Docente", "Estudiante"];
            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync (role))
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }

            var adminEmail = config["AdminSettings:Email"] ?? "admin@universidad.com";
            var adminPassword = config["AdminSettings:Password"];

            // Si no hay contraseña configurada, NO creamos el admin (seguridad por defecto)
            if (string.IsNullOrEmpty(adminPassword)) return;


            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Nombre = "Admin",
                    Apellido = "Universidad",
                    Email = adminEmail,
                    EmailConfirmed = true // Importante para producción
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

        }
    }
}

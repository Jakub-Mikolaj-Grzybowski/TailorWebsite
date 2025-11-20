using Microsoft.AspNetCore.Identity;
using TailorWebsite.Model.DataModels;

namespace TailorWebsite.Web;

public static class DbSeeder
{
    public static async Task SeedRoles(RoleManager<Role> roleManager)
    {
        foreach (var roleValue in Enum.GetValues(typeof(RoleValue)))
        {
            var roleName = roleValue.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new Role(roleName, (RoleValue)roleValue));
            }
        }
    }

    public static async Task SeedAdminUser(UserManager<User> userManager)
    {
        const string adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser != null)
        {
            await userManager.DeleteAsync(adminUser);
        }

        var newAdminUser = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
            Name = "Admin",
            Surname = "User",
            EmailConfirmed = true,
            RegistrationDate = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(newAdminUser, "Admin123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdminUser, RoleValue.Admin.ToString());
        }
    }
}

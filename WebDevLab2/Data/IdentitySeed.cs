namespace WebDevLab2.Data
{
    using Microsoft.AspNetCore.Identity;
    using WebDevLab2.Model;

    namespace WebAPI.Models
    {
        public static class IdentitySeed
        {
            public static async Task CreateUserRoles(IServiceProvider serviceProvider)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                // Создание ролей администратора и пользователя
                if (await roleManager.FindByNameAsync("admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                }
                if (await roleManager.FindByNameAsync("player") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("player"));
                }
                if (await roleManager.FindByNameAsync("developer") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("developer"));
                }
                // Создание Администратора
                string adminLogin = "admin";
                string adminPassword = "Admin<3";
                if (await userManager.FindByNameAsync(adminLogin) == null)
                {
                    User admin = new User { UserName = adminLogin };
                    IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "admin");
                    }
                }
                // Создание Пользователя
                string userLogin = "user";
                string userPassword = "User<3";
                if (await userManager.FindByNameAsync(userLogin) == null)
                {
                    User user = new User { UserName = userLogin };
                    IdentityResult result = await userManager.CreateAsync(user, userPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "player");
                    }
                }
            }
        }
    }
}

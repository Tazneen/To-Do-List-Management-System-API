using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task_Management.Data.Models;
using Task_Management.Data.StaticData;

namespace Task_Management.Data.Data
{
    public static class SeedData
    {
        public static async System.Threading.Tasks.Task RoleCreation(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserRole.Admin))
            {
                var adminRole = new IdentityRole() { Name = UserRole.Admin, NormalizedName = UserRole.Admin.ToUpper() };
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync(UserRole.User))
            {
                var userRole = new IdentityRole() { Name = UserRole.User, NormalizedName = UserRole.User.ToUpper() };
                await roleManager.CreateAsync(userRole);
            }

        }

        public static async System.Threading.Tasks.Task UserCreation(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();


            var admin = new ApplicationUser()
            {
                FirstName = "Ronald",
                LastName = "Roni Saha",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@enkaizen.com",
                NormalizedEmail = "ADMIN@ENKAIZEN.COM",
                CreatedDate = DateTime.Now,
                UserType = UserRole.Admin,
                Gender = Gender.Male
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");


            var user = new ApplicationUser()
            {
                FirstName = "Tazneen",
                LastName = "Sultana",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@enkaizen.com",
                NormalizedEmail = "USER@ENKAIZEN.COM",
                CreatedDate = DateTime.Now,
                UserType = UserRole.User,
                Gender = Gender.Female
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "User@123");

            await userManager.CreateAsync(admin);
            await userManager.AddToRoleAsync(admin, UserRole.Admin);


            await userManager.CreateAsync(user);
            await userManager.AddToRoleAsync(user, UserRole.User);
        }

        public static async System.Threading.Tasks.Task DatabaseSeedingAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.Migrate();


            //------seeding----------
            await RoleCreation(roleManager);

            await UserCreation(context, userManager);

        }

    }
}

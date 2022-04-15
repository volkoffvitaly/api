using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Models;

namespace TinkoffWatcher_Api
{
    public static class DbMigration
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();
                ConfigureIdentity(scope).GetAwaiter().GetResult();
            }

            return webHost;
        }

        private static async Task ConfigureIdentity(IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var adminsRole = await roleManager.FindByNameAsync(ApplicationRoles.Administrators);
            if (adminsRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Administrators));
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create {ApplicationRoles.Administrators} role.");
                }

                adminsRole = await roleManager.FindByNameAsync(ApplicationRoles.Administrators);
            }

            var usersRole = await roleManager.FindByNameAsync(ApplicationRoles.Users);
            if (usersRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Users));
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create {ApplicationRoles.Users} role.");
                }

                usersRole = await roleManager.FindByNameAsync(ApplicationRoles.Users);
            }

            var curatorsRole = await roleManager.FindByNameAsync(ApplicationRoles.Curator);
            if (curatorsRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Curator));
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create {ApplicationRoles.Curator} role.");
                }

                curatorsRole = await roleManager.FindByNameAsync(ApplicationRoles.Users);
            }

            var studentsRole = await roleManager.FindByNameAsync(ApplicationRoles.Student);
            if (studentsRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Student));
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create {ApplicationRoles.Student} role.");
                }

                studentsRole = await roleManager.FindByNameAsync(ApplicationRoles.Student);
            }

            var representativeCompanyRole = await roleManager.FindByNameAsync(ApplicationRoles.CompanyAgent);
            if (representativeCompanyRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.CompanyAgent));
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create {ApplicationRoles.CompanyAgent} role.");
                }

                representativeCompanyRole = await roleManager.FindByNameAsync(ApplicationRoles.Users);
            }

            var representativeSchoolRole = await roleManager.FindByNameAsync(ApplicationRoles.SchoolAgent);
            if (representativeSchoolRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.SchoolAgent));
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create {ApplicationRoles.SchoolAgent} role.");
                }

                representativeSchoolRole = await roleManager.FindByNameAsync(ApplicationRoles.Users);
            }

            var adminUser = await userManager.FindByNameAsync("admin@localhost.local");
            if (adminUser == null)
            {
                var userResult = await userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@localhost.local",
                    Email = "admin@localhost.local"
                }, "AdminPass123!");
                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create admin@localhost.local user");
                }

                adminUser = await userManager.FindByNameAsync("admin@localhost.local");
            }

            if (!await userManager.IsInRoleAsync(adminUser, adminsRole.Name))
            {
                await userManager.AddToRoleAsync(adminUser, adminsRole.Name);
            }
        }

    }
}

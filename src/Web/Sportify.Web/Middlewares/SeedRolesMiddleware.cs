﻿namespace Sportify.Web.Middlewares
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Constants;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var dbContext = serviceProvider.GetService<SportifyDbContext>();

            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(userManager, roleManager);
            }

            await this.next(context);
        }

        private async Task SeedRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Administrator));
            await roleManager.CreateAsync(new IdentityRole(Role.Editor));
            await roleManager.CreateAsync(new IdentityRole(Role.User));
        }
    }
}
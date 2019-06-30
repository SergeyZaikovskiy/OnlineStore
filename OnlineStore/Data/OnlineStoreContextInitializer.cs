using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data
{
    /// <summary>
    /// Класс для заполнения быза данных
    /// </summary>
    public class OnlineStoreContextInitializer
    {
        private readonly OnlineStoreContext _db;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public OnlineStoreContextInitializer(OnlineStoreContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync() {
            await _db.Database.MigrateAsync();

            await InitializeIdentityAsync();

            if (await _db.Products.AnyAsync())
                return;
        }

        private async Task InitializeIdentityAsync()
        {
            if (!await _roleManager.RoleExistsAsync(User.RoleAdmin))
                await _roleManager.CreateAsync(new IdentityRole(User.RoleAdmin));


            if (!await _roleManager.RoleExistsAsync(User.RoleUser))
                await _roleManager.CreateAsync(new IdentityRole(User.RoleUser));

            if (await _userManager.FindByNameAsync(User.AdminUserName) == null)
            {
                var admin = new User
                {
                    UserName = User.AdminUserName,
                    Email = $"(User.AdminUserName)@server.ru"
                };

                var creation_result = await _userManager.CreateAsync(admin, User.DefualtAdminPassword);

                if (creation_result.Succeeded)
                    await _userManager.AddToRoleAsync(admin, User.RoleAdmin);
            }


        }
    }
}

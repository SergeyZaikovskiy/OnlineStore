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

            //заполняем базу Секциями
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Sections.AddRangeAsync(TestData.Sections);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                transaction.Commit();
            }

            //заполняем базу Категориями
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Categories.AddRangeAsync(TestData.Categories);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Categories] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Categories] OFF");

                transaction.Commit();
            }

            //заполняем базу Брендами
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Brands.AddRangeAsync(TestData.Brands);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                transaction.Commit();
            }

            //заполняем базу соответствями Секции и категорий
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.SectionToCategory.AddRangeAsync(TestData.SectionsToCategories);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[SectionToCategory] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[SectionToCategory] OFF");

                transaction.Commit();
            }

            //заполняем базу соответствями Категорий и бренов
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.CategoryToBrand.AddRangeAsync(TestData.CategoryToBrands);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[CategoryToBrand] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[CategoryToBrand] OFF");

                transaction.Commit();
            }


            //Заполняем базу файлов
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Files.AddRangeAsync(TestData.Files);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Files] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Files] OFF");

                transaction.Commit();
            }
            
            //заполняем базу Товарами
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Products.AddRangeAsync(TestData.Products);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }

            //заполнаем базу Должностей
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Positions.AddRangeAsync(TestData.Positions);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Positions] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Positions] OFF");

                transaction.Commit();
            }

            //заполняем базу Сотрудниками
            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Employees.AddRangeAsync(TestData.Employees);
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Employees] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Employees] OFF");

                transaction.Commit();
            }            
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

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities.EmployeesEntities;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Domain.Entities.ServiceEntity;
using OnlineStore.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DAL.Context
{
    /// <summary>
    /// Класс для создания базы данных
    /// </summary>
   public class OnlineStoreContext: IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Category> Categories{ get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Employee> Employees { get; set; }        

        public DbSet<FileModel> Files { get; set; }

        //public DbSet<SectionToCategory> SectionToCategory { get; set; }

        //public DbSet<CategoryToBrand> CategoryToBrand { get; set; }

        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options) : base(options)
        {

        }

        /// <summary>
        /// Настройка ключей и сотношения многие ко многим и соотношения один ко многим
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Конфигурация Секции к Категориям много ко многим
            modelBuilder.Entity<SectionToCategory>()
            .HasKey(secToCat => new { secToCat.SectionId, secToCat.CategoryId});

            modelBuilder.Entity<SectionToCategory>()
                .HasOne(secToCat => secToCat.Section)
                .WithMany(sec => sec.SecTocCat)
                .HasForeignKey(secToCat => secToCat.SectionId);

            modelBuilder.Entity<SectionToCategory>()
                .HasOne(secToCat => secToCat.Category)
                .WithMany(cat => cat.CatToSec)
                .HasForeignKey(secToCat => secToCat.CategoryId);

            //Конфигурация Бренды к Категориям много ко многим
            modelBuilder.Entity<CategoryToBrand>()
           .HasKey(catToBrand => new { catToBrand.CategoryId, catToBrand.BrandId });

            modelBuilder.Entity<CategoryToBrand>()
                .HasOne(catToBrand => catToBrand.Category)
                .WithMany(cat => cat.CatToBrand)
                .HasForeignKey(catToBrand => catToBrand.CategoryId);

            modelBuilder.Entity<CategoryToBrand>()
                .HasOne(brandToCat => brandToCat.Category)
                .WithMany(brand => brand.CatToBrand)
                .HasForeignKey(brandToCat => brandToCat.BrandId);

            ///Конфигурация Секции к Товарам много к одному
            modelBuilder.Entity<Section>()
                .HasMany(section => section.Products)
                .WithOne(product => product.Section)
                .HasForeignKey(product => product.SectionId);

            ///Конфигурация Категорий к Товарам много к одному
            modelBuilder.Entity<Category>()
                .HasMany(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId);

            ///Конфигурация Брендов к Товарам много к одному
            modelBuilder.Entity<Brand>()
                .HasMany(brand => brand.Products)
                .WithOne(product => product.Brand)
                .HasForeignKey(product => product.BrandId);

            ///Конфигурация Должностей (Position) к Сотрудникам много к одному
            modelBuilder.Entity<Position>()
                .HasMany(position => position.Employees)
                .WithOne(employee => employee.Position)
                .HasForeignKey(product => product.PositionId);
        }
    }
}

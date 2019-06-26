using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities.EmployeesEntities;
using OnlineStore.Domain.Entities.ProductsEntities;
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

        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Brand>().HasMany(b => b.Sections).WithMany(s => s.Brands);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Domain.Entities.ServiceEntity;
using OnlineStore.Infrastructure.Interfeices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Implementations
{
    /// <summary>
    /// Реализация сервиса работы с Товарами и базой данных
    /// </summary>
    public class SqlProductData : IProductData
    {
        private readonly OnlineStoreContext db;

        public SqlProductData(OnlineStoreContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Список брендов
        /// </summary>
        /// <returns></returns>
        public IQueryable<Brand> GetBrands() => db.Brands
            .Include(b => b.Products);

        /// <summary>
        /// Получить бренд по id
        /// </summary>
        /// <param name="id">id бренда</param>
        /// <returns></returns>
        public Brand GetbrandById(int? id) => db.Brands.Include(b => b.Products)
            .FirstOrDefault(br => br.id == id);

        /// <summary>
        /// Список секций
        /// </summary>
        /// <returns></returns>
        public IQueryable<Section> GetSections() => db.Sections
            .Include(s => s.Products);

        /// <summary>
        /// Получить брендл по id
        /// </summary>
        /// <param name="id">id бренда</param>
        /// <returns></returns>
        public Section GetSectionById(int? id) => db.Sections.Include(b => b.Products)
            .FirstOrDefault(sec => sec.id == id);


        /// <summary>
        /// Список Категорий
        /// </summary>
        /// <returns></returns>
        public IQueryable<Category> GetCategories() => db.Categories
            .Include(c => c.Products);

        /// <summary>
        /// Получить Категории по id
        /// </summary>
        /// <param name="id">id категории</param>
        /// <returns></returns>
        public Category GetCategoryByIdCategory(int? id) => db.Categories.Include(cat => cat.Products)
            .FirstOrDefault(cat => cat.id == id);


        /// <summary>
        /// Получить Категории по id секции
        /// </summary>
        /// <param name="id">id категории</param>
        /// <returns></returns>
        public IQueryable<SectionToCategory> GetCategoryByIdSection(int? idSection) => db.SectionToCategory.Where(catToSec=>catToSec.SectionId==idSection).Include(catToSec=>catToSec.Category);
           

        /// <summary>
        /// Получить все изображения из базы
        /// </summary>
        /// <returns></returns>
        public IQueryable<FileModel> GetFiles() => db.Files;

        /// <summary>
        /// Получить модель файла
        /// </summary>
        /// <param name="id">Id файла</param>
        /// <returns></returns>
        public FileModel GetFileById(int id) => db.Files.FirstOrDefault(f => f.id == id);

        /// <summary>
        /// Список товаров
        /// </summary>
        /// <param name="productFilter">Фильтр для товаров</param>
        /// <returns></returns>
        public IQueryable<Product> GetProducts(ProductFilter productFilter)
        {
            IQueryable<Product> products = db.Products
                .Include(prod => prod.Brand)
                .Include(prod => prod.Section)
                .Include(prod => prod.FileModel);

            if (productFilter is null)
                return products;

            if (productFilter.SectionId != null)
                products = products.Where(p => p.SectionId == productFilter.SectionId);

            //if (productFilter.BrandId != null)
            //    products = products.Where(p => p.BrandId == productFilter.BrandId);

            return products;
        }

        /// <summary>
        /// Получить товар по id
        /// </summary>
        /// <param name="id">id товара</param>
        /// <returns></returns>
        public Product GetProductById(int? id)
        {
            return db.Products
                 .Include(pr => pr.Brand)
                 .Include(pr => pr.Section)
                 .Include(pr => pr.FileModel)
                 .FirstOrDefault(pr => pr.id == id);
        }

        /// <summary>
        /// Удалить товар из базы
        /// </summary>
        /// <param name="id">id товара</param>
        public void RemoveProduct(int? id)
        {
            var prod = db.Products.FirstOrDefault(pr => pr.id == id);
            if (prod is null) return;

            db.Products.Remove(prod);
            db.SaveChanges();
        }

        /// <summary>
        /// Изменить описание товара в базе
        /// </summary>
        /// <param name="product">Товар для изменения</param>
        public void UpdateInfoProduct(Product product)
        {
            if (product is null) return;
            if (product.id > 0)
            {
                db.Products.Update(product);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Добавить товар в базу
        /// </summary>
        /// <param name="product">Товар для добавления</param>
        public void AddProduct(Product product)
        {
            if (product is null) return;
            db.Products.Add(product);
            db.SaveChanges();
        }

        /// <summary>
        /// Добавить файл на сервер
        /// </summary>
        /// <param name="fileModel">Файл</param>
        public void AddFileToBase(FileModel fileModel)
        {
            if (fileModel is null) return;
            db.Files.Add(fileModel);
            db.SaveChanges();
        }

    }
}
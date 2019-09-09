using Microsoft.EntityFrameworkCore;
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
        public IQueryable<Brand> GetBrands(ProductFilter productFilter)
        {            
            if (productFilter != null)
            {
                var brandsOne = db.CategoryToBrand.Where(br => br.CategoryId == productFilter.CategoryId).Select(c => c.Brand);
                var brandsTwo = db.SectionToBrands.Where(br => br.SectionId == productFilter.SectionId).Select(c => c.Brand);
                var  brandResult = brandsOne.Intersect(brandsTwo);
                return brandResult;
            }
            else
                return db.Brands;

            
        }


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
        public IQueryable<Category> GetCategories(ProductFilter productFilter)
        {          

            if (productFilter is null)
            {
                var categories = db.Categories.Include(p => p.Products);                
                return categories;
            }

            var cats = db.SectionToCategory
                .Where(sc => sc.SectionId == productFilter.SectionId)
                .Select(c => c.Category);                                       
                                                      
            return cats;

        }

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
        public IQueryable<SectionToCategory> GetCategoryByIdSection(int? idSection) => db.SectionToCategory
            .Where(catToSec=>catToSec.SectionId==idSection)
            .Include(catToSec=>catToSec.Category);
           

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
        public IQueryable<Product> GetProducts(ProductFilter productFilter, int countProducts = 0)
        {
            IQueryable<Product> products = db.Products
                .Include(prod => prod.Brand)
                .Include(prod => prod.Section)
                .Include(prod => prod.Category)
                .Include(prod => prod.FileModel);


            if (productFilter is null)
                return products;

            if (!String.IsNullOrEmpty(productFilter.Name))
                products = products.Where(p => p.Name.Contains(productFilter.Name));

            if (productFilter.SectionId != null && productFilter.SectionId > 0)
                products = products.Where(p => p.SectionId == productFilter.SectionId);

            if (productFilter.CategoryId != null && productFilter.CategoryId > 0)
                products = products.Where(p => p.CategoryId == productFilter.CategoryId);

            if (productFilter.BrandIdCollection != null && productFilter.BrandIdCollection.Count() > 0)
                products = products.Where(p => productFilter.BrandIdCollection.Contains((int)p.BrandId));

            if (productFilter.MinPrice!=null && productFilter.MaxPrice!=null && (productFilter.MinPrice <productFilter.MaxPrice))
                products = products.Where(p => (p.Price > productFilter.MinPrice && p.Price < productFilter.MaxPrice));
            

            if (countProducts > 0)
                return products.Take(countProducts);

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
                 .Include(pr=>pr.Category)
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
        public void UpdateInfoOrAddProduct(Product product)
        {
            if (product is null) return;          

            if (product.id > 0)
            {
                db.Products.Update(product);
            }//Обновить данные о товаре
            else { db.Products.Add(product); }//Добавить новый товар в базу

            #region Обновить связи между категориями, секциями и брендами

            var linkSecToCat = db.SectionToCategory.FirstOrDefault(seccat => seccat.CategoryId == product.CategoryId && seccat.SectionId == product.SectionId);
            if (linkSecToCat == null) { db.SectionToCategory.Add(new SectionToCategory { SectionId = product.SectionId, CategoryId = product.CategoryId }); }
            var linkCatToBr = db.CategoryToBrand.FirstOrDefault(catbr => catbr.CategoryId == product.CategoryId && catbr.BrandId == product.BrandId);
            if (linkCatToBr == null) { db.CategoryToBrand.Add(new CategoryToBrand { BrandId = (int)product.BrandId, CategoryId = product.CategoryId }); }
            var linkSecToBr = db.SectionToBrands.FirstOrDefault(secbr => secbr.SectionId == product.SectionId && secbr.BrandId == product.BrandId);
            if (linkSecToBr == null) { db.SectionToBrands.Add(new SectionToBrands { SectionId = product.SectionId, BrandId = (int)product.BrandId }); }

            #endregion

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

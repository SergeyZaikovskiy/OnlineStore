using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Domain.Entities.ServiceEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Interfeices
{

    /// <summary>
    /// Сервис для товаров
    /// </summary>
    public interface IProductData
    {
        /// <summary>
        /// Получаем секции
        /// </summary>
        /// <returns></returns>
        IQueryable<Section> GetSections();

        /// <summary>
        /// Получить секцию по id
        /// </summary>
        /// <param name="id">id секции</param>
        /// <returns></returns>
        Section GetSectionById(int? id);



        /// <summary>
        /// Получаем Категории
        /// </summary>
        /// <returns></returns>
        IQueryable<Category> GetCategories(ProductFilter productFilter);

        /// <summary>
        /// Получить Категория по id
        /// </summary>
        /// <param name="idCategory">id Категории</param>
        /// <returns></returns>
        Category GetCategoryByIdCategory(int? idCategory);

        /// <summary>
        /// Получить Категория по id Секции для запроса many to many
        /// </summary>
        /// <param name="idSection">id Категории</param>
        /// <returns></returns>
        IQueryable<SectionToCategory> GetCategoryByIdSection(int? idSection);

        /// <summary>
        /// Получаем бренды
        /// </summary>
        /// <returns></returns>
        IQueryable<Brand> GetBrands(ProductFilter productFilter);

        /// <summary>
        /// Получить Бренд по id
        /// </summary>
        /// <param name="id">id бренда</param>
        /// <returns></returns>
        Brand GetbrandById(int? id);

        /// <summary>
        /// Получить все изображения из базы
        /// </summary>
        /// <returns></returns>
        IQueryable<FileModel> GetFiles();

        /// <summary>
        /// Получить ссылку на файл по id
        /// </summary>
        /// <param name="id">id файла</param>
        FileModel GetFileById(int id);

        /// <summary>
        /// Получаем товары по фильтру
        /// </summary>
        /// <param name="productFilter"></param>
        /// <returns></returns>
        IQueryable<Product> GetProducts(ProductFilter productFilter, int countProducts = 0);

        /// <summary>
        /// Получаем товар по Id
        /// </summary>
        /// <param name="id">Id товара</param>
        /// <returns></returns>
        Product GetProductById(int? id);

        /// <summary>
        /// Добавляем товар в базу
        /// </summary>
        /// <param name="emp">Товар для удаления</param>
        void AddProduct(Product product);

        /// <summary>
        /// Удаляем товар из базы
        /// </summary>
        /// <param name="id">Id товара</param>
        void RemoveProduct(int? id);

        /// <summary>
        /// Обновить информации о товаре
        /// </summary>
        /// <param name="id">id товара</param>
        void UpdateInfoProduct(Product product);

        /// <summary>
        /// Добавить файл на сервер
        /// </summary>
        /// <param name="fileModel">Файл</param>
        void AddFileToBase(FileModel fileModel);
    }
}

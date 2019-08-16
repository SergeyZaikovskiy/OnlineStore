using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.ViewModels;
using OnlineStore.ViewModels.Product;

namespace OnlineStore.Components
{
    /// <summary>
    /// Компонент для области брендов
    /// Вызывает компонент Brands
    /// </summary>
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData productDate)
        {
            _ProductData = productDate;
        }

        /// <summary>
        /// Загрузка и отображения представления для Компонента Brands
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(CatalogViewModel catalogViewModel)
        {
            var brands =await Task.Run(()=> GetBrands(catalogViewModel));

            var brandsEnumerable = new BrandsEnumerableViewModel {SectionID = catalogViewModel.SectionId, CategoryID = catalogViewModel.CategoryId, Brands=brands.ToList(), SortValue = catalogViewModel.SortViewModel.Current };

            return View(brandsEnumerable);
        }

        private IQueryable<BrandViewModel> GetBrands(CatalogViewModel catalogViewModel)
        {
            //var ChoosenBrands = catalogViewModel.Brands
            //    .Where(br => br.Choosen)
            //    .Select(x => (int?)x.Id);                

            ProductFilter productFilter = new ProductFilter { SectionId = catalogViewModel.SectionId, CategoryId = catalogViewModel.CategoryId,
                BrandIdCollection = catalogViewModel.Brands
            };
       
            var brands =  _ProductData.GetBrands(productFilter);                       

            var BrandsViewModels = brands.Select(brand => brand.CreateViewModel()).ToList();

            //Для заполнения количества товара по брендам
            for (int i = 0; i < BrandsViewModels.Count; i++)
            {
                List<int> brand = new List<int> { (int)BrandsViewModels[i].Id };
                ProductFilter pf = new ProductFilter { SectionId = productFilter.SectionId, CategoryId = productFilter.CategoryId, BrandIdCollection = brand };
                var countGoods = _ProductData.GetProducts(pf).Count();
                if (catalogViewModel.Brands.Contains(BrandsViewModels[i].Id))
                {
                    BrandsViewModels[i].Choosen = true;
                }
                BrandsViewModels[i].ProductsCount = countGoods;
            }//только для заполнения количества товара для каждого бренда и отметки ввыранности                           

            return BrandsViewModels.AsQueryable();
        }
    }
}

using OnlineStore.Domain.SortsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// View модель сортировки товаров, добавляется к модели товаров
    /// </summary>
    public class SortViewModelForProduct
    {
        public string NameSort { get; set; }
        public string SectionSort { get; set; }
        public string CategorySort { get; set; }
        public string BrandSort { get; set; }
        public string PriceSort { get; set; }
        public string Current { get; set; }

        //Иногда нужно просто сохранять текущую сортировку
        public bool NeedChangeSort { get; set; } = true;

        public bool Up { get; set; }

        public SortViewModelForProduct(string SortType)
        {
            NameSort = SortEntityForProducts.NameAsc;
            SectionSort = SortEntityForProducts.SectionAsc;
            CategorySort = SortEntityForProducts.CategoryAsc;
            BrandSort = SortEntityForProducts.BrandAsc;
            PriceSort = SortEntityForProducts.PriceAsc;

            Up = true;

            if (SortType == SortEntityForProducts.NameAsc
                || SortType == SortEntityForProducts.SectionAsc
                || SortType == SortEntityForProducts.CategoryAsc
                || SortType == SortEntityForProducts.BrandAsc
                || SortType == SortEntityForProducts.PriceDes)
            {
                Up = false;
            }

            if (NeedChangeSort)
            {
                switch (SortType)
                {
                    case SortEntityForProducts.NameDes:
                        Current = NameSort = SortEntityForProducts.NameAsc;
                        break;

                    case SortEntityForProducts.SectionAsc:
                        Current = SectionSort = SortEntityForProducts.SectionDes;
                        break;
                    case SortEntityForProducts.SectionDes:
                        Current = SectionSort = SortEntityForProducts.SectionAsc;
                        break;

                    case SortEntityForProducts.CategoryAsc:
                        Current = BrandSort = SortEntityForProducts.CategoryDes;
                        break;
                    case SortEntityForProducts.CategoryDes:
                        Current = BrandSort = SortEntityForProducts.CategoryAsc;
                        break;

                    case SortEntityForProducts.BrandAsc:
                        Current = BrandSort = SortEntityForProducts.BrandDes;
                        break;
                    case SortEntityForProducts.BrandDes:
                        Current = BrandSort = SortEntityForProducts.BrandAsc;
                        break;

                    case SortEntityForProducts.PriceAsc:
                        Current = PriceSort = SortEntityForProducts.PriceDes;
                        break;
                    case SortEntityForProducts.PriceDes:
                        Current = PriceSort = SortEntityForProducts.PriceAsc;
                        break;

                    default:
                        Current = NameSort = SortEntityForProducts.NameDes;
                        break;
                }
            }
        }

    }

}


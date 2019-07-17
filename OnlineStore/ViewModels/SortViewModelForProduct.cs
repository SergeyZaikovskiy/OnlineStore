using OnlineStore.Domain.Enums;
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
        public EnumSortForProducts NameSort { get; set; }
        public EnumSortForProducts SectionSort { get; set; }
        public EnumSortForProducts BrandSort { get; set; }
        public EnumSortForProducts PriceSort { get; set; }

        public EnumSortForProducts Current { get; set; }

        public bool Up { get; set; }

        public SortViewModelForProduct(EnumSortForProducts SortEnum)
        {
            NameSort = EnumSortForProducts.NameAsc;
            SectionSort = EnumSortForProducts.SectionAsc;
            BrandSort = EnumSortForProducts.BrandAsc;
            PriceSort = EnumSortForProducts.PriceAsc;

            Up = true;

            if (SortEnum == EnumSortForProducts.NameAsc || SortEnum == EnumSortForProducts.SectionAsc
                || SortEnum == EnumSortForProducts.BrandAsc
                || SortEnum == EnumSortForProducts.PriceAsc)
            {
                Up = false;
            }

            switch (SortEnum)
            {
                case EnumSortForProducts.NameDes:
                    Current = NameSort = EnumSortForProducts.NameAsc;
                    break;

                case EnumSortForProducts.SectionAsc:
                    Current = SectionSort = EnumSortForProducts.SectionDes;
                    break;
                case EnumSortForProducts.SectionDes:
                    Current = SectionSort = EnumSortForProducts.SectionAsc;
                    break;

                case EnumSortForProducts.BrandAsc:
                    Current = BrandSort = EnumSortForProducts.BrandDes;
                    break;
                case EnumSortForProducts.BrandDes:
                    Current = BrandSort = EnumSortForProducts.BrandAsc;
                    break;

                case EnumSortForProducts.PriceAsc:
                    Current = PriceSort = EnumSortForProducts.PriceDes;
                    break;
                case EnumSortForProducts.PriceDes:
                    Current = PriceSort = EnumSortForProducts.PriceAsc;
                    break;

                default:
                    Current = NameSort = EnumSortForProducts.NameDes;
                    break;
            }
        }

    }

}


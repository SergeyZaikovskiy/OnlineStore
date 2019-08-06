using OnlineStore.Domain.SortsEntities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.SortsEntities
{
    public class SortEntityForProducts : BaseSortEntity
    {
        public const string SectionAsc = "SectionAsc";
        public const string SectionDes = "SectionDes";

        public const string CategoryAsc = "CategoryAsc";
        public const string CategoryDes = "CategoryDes";

        public const string BrandAsc = "BrandAsc";
        public const string BrandDes = "BrandDes";

        public const string PriceAsc = "PriceAsc";
        public const string PriceDes = "PriceDes";
    }
   
}

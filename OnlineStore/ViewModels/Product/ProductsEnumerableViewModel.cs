﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Модель представления товаров с возможностью сортировки
    /// </summary>

    public class ProductsEnumerableViewModel
    {
        public IEnumerable<ProductViewModel> products { get; set; }
        public SortViewModelForProduct SortViewModel { get; set; }

    }
}
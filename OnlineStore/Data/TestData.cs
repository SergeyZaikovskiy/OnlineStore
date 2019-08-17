using OnlineStore.Domain.Entities.EmployeesEntities;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Domain.Entities.ServiceEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data
{
    /// <summary>
    /// Класс тестового набора данных
    /// </summary>
    public class TestData
    {
        /// <summary>
        /// Тестовый набор Секций товаров
        /// </summary>
        public static List<Section> Sections { get; } = new List<Section> {
            new Section {id = 1, Name = "Мужчины", Order = 0},
            new Section {id = 2, Name = "Женщины", Order = 0},
            new Section {id = 3, Name = "Спорт", Order = 2},
            new Section {id = 4, Name = "Аксессуары", Order = 3}
        };

        /// <summary>
        /// Тестовый набор Категорий товаров
        /// </summary>
        public static List<Category> Categories { get; } = new List<Category>
        {
            //одежда мужская и женская
            new Category{id = 1, Name="Брюки и шорты",Order = 0},
            new Category{id = 2, Name="Блузки",Order = 0},
            new Category{id = 3, Name="Водолазки",Order = 0},
            new Category{id = 4, Name="Верхняя одежда",Order = 0},
            new Category{id = 5, Name="Джинсы",Order = 0},
            new Category{id = 6, Name="Футболки и майки",Order = 0},
            new Category{id = 7, Name="Рубашки",Order = 0},
            new Category{id = 8, Name="Одежда для дома",Order = 0},
            new Category{id = 9, Name="Костюмы",Order = 0},
            new Category{id = 10, Name="Джемперы и кардиганы",Order = 0},
            new Category{id = 11, Name="Платья",Order = 0},
            new Category{id = 12, Name="Туники",Order = 0},
            new Category{id = 13, Name="Белье",Order = 0},

           //Спорт
            new Category{id = 14, Name="Футбол",Order = 0},
            new Category{id = 15, Name="Хоккей",Order = 0},
            new Category{id = 16, Name="Бег",Order = 0},
            new Category{id = 17, Name="Велосипеды",Order = 0},
            new Category{id = 18, Name="Туризм",Order = 0},
            new Category{id = 19, Name="Фитнес",Order = 0},
            new Category{id = 20, Name="Баскетбол",Order = 0},

            //Аксессуары
            new Category{id = 21, Name="Сумки",Order = 0},
            new Category{id = 22, Name="Шляпы",Order = 0},
            new Category{id = 23, Name="Кепки",Order = 0},
            new Category{id = 24, Name="Перчатки",Order = 0},
            new Category{id = 25, Name="Бижутерия",Order = 0}
        };

        /// <summary>
        /// Соотношение Секций к Категориям, для рапроса многие ко многим
        /// </summary>
        public static List<SectionToCategory> SectionsToCategories { get; } = new List<SectionToCategory>
        {
            new SectionToCategory{id =1 ,SectionId = 1, CategoryId = 1},
            new SectionToCategory{id =2 ,SectionId = 1, CategoryId = 3},
            new SectionToCategory{id =3 ,SectionId = 1, CategoryId = 4},
            new SectionToCategory{id =4 ,SectionId = 1, CategoryId = 5},
            new SectionToCategory{id =5 ,SectionId = 1, CategoryId = 6},
            new SectionToCategory{id =6 ,SectionId = 1, CategoryId = 7},
            new SectionToCategory{id =7 ,SectionId = 1, CategoryId = 8},
            new SectionToCategory{id =8 ,SectionId = 1, CategoryId = 9},
            new SectionToCategory{id =9 ,SectionId = 1, CategoryId = 10},
            new SectionToCategory{id =10 ,SectionId = 1, CategoryId = 13},

            new SectionToCategory{id =11 ,SectionId = 2, CategoryId = 1},
            new SectionToCategory{id =12 ,SectionId = 2, CategoryId = 2},
            new SectionToCategory{id =13 ,SectionId = 2, CategoryId = 3},
            new SectionToCategory{id =14 ,SectionId = 2, CategoryId = 4},
            new SectionToCategory{id =15 ,SectionId = 2, CategoryId = 5},
            new SectionToCategory{id =16 ,SectionId = 2, CategoryId = 6},
            new SectionToCategory{id =17 ,SectionId = 2, CategoryId = 7},
            new SectionToCategory{id =18,SectionId = 2, CategoryId = 8},
            new SectionToCategory{id =19 ,SectionId = 2, CategoryId = 9},
            new SectionToCategory{id =20 ,SectionId =2, CategoryId = 10},
            new SectionToCategory{id =21 ,SectionId = 2, CategoryId = 11},
            new SectionToCategory{id =22,SectionId = 2, CategoryId = 12},
            new SectionToCategory{id =23 ,SectionId = 2, CategoryId = 13},


            new SectionToCategory{id =24 ,SectionId = 3, CategoryId = 14},
            new SectionToCategory{id =25 ,SectionId = 3, CategoryId = 15},
            new SectionToCategory{id =26 ,SectionId = 3, CategoryId = 16},
            new SectionToCategory{id =27 ,SectionId = 3, CategoryId = 17},
            new SectionToCategory{id =28 ,SectionId = 3, CategoryId = 18},
            new SectionToCategory{id =29 ,SectionId = 3, CategoryId = 19},
            new SectionToCategory{id =30 ,SectionId = 3, CategoryId = 20},

            new SectionToCategory{id =31 ,SectionId = 4, CategoryId = 21},
            new SectionToCategory{id =32 ,SectionId = 4, CategoryId = 22},
            new SectionToCategory{id =33 ,SectionId = 4, CategoryId = 23},
            new SectionToCategory{id =34 ,SectionId = 4, CategoryId = 24},
            new SectionToCategory{id =35 ,SectionId = 4, CategoryId = 25},

        };

        /// <summary>
        /// Тестовый набор Брендов товаров
        /// </summary>
        public static List<Brand> Brands { get; } = new List<Brand>
        {
            new Brand {id = 1, Name = "Nike", Order = 0},
            new Brand {id = 2, Name = "Adidas", Order = 1},
            new Brand {id = 3, Name = "Puma", Order = 2},
            new Brand {id = 4, Name = "New Balance", Order = 3},
            new Brand {id = 5, Name = "Reebok", Order = 4},
            new Brand {id = 6, Name = "Geox", Order = 5},
            new Brand {id = 7, Name = "Finn Flare", Order = 6},
            new Brand {id = 8, Name = "ТВОЕ", Order = 5},
            new Brand {id = 9, Name = "Levi's", Order = 5},
            new Brand {id = 10, Name = "Marco", Order = 5},
            new Brand {id = 11, Name = "U.S. polo", Order = 5},
            new Brand {id = 12, Name = "Cleo", Order = 5},
            new Brand {id = 13, Name = "FORLIFE", Order = 5},
            new Brand {id = 14, Name = "KUPPER", Order = 5},
            new Brand {id = 15, Name = "Lacoste", Order = 5},
            new Brand {id = 16, Name = "H&M", Order = 5},
            new Brand {id = 17, Name = "Zara", Order = 5},
            new Brand {id = 18, Name = "Gucci", Order = 5},
            new Brand {id = 19, Name = "Burberry", Order = 5},
            new Brand {id = 20, Name = "Versace", Order = 5},
            new Brand {id = 21, Name = "Prada", Order = 5},
            new Brand {id = 22, Name = "Dior", Order = 5},
            new Brand {id = 23, Name = "Calvin Klein", Order = 5},
            new Brand {id = 24, Name = "Guess", Order = 5},
            new Brand {id = 25, Name = "Boudestijn", Order = 5},
            new Brand {id = 26, Name = "Oddmolly", Order = 5},
            new Brand {id = 27, Name = "Ronhill", Order = 5},
            new Brand {id = 28, Name = "Albiro", Order = 5},
            new Brand {id = 29, Name = "Grune Erde", Order = 5},
            new Brand {id = 30, Name = "Acne", Order = 5}
        };

        public static List<CategoryToBrand> CategoryToBrands { get; } = new List<CategoryToBrand>
        {

#region Катерогии одежды
            new CategoryToBrand{id=1, CategoryId = 1 , BrandId=1},
            new CategoryToBrand{id=2, CategoryId = 1 , BrandId=2},
            new CategoryToBrand{id=3, CategoryId = 1 , BrandId=3},
            new CategoryToBrand{id=4, CategoryId = 1 , BrandId=4},
            new CategoryToBrand{id=5, CategoryId = 1 , BrandId=5},
            new CategoryToBrand{id=6, CategoryId = 1 , BrandId=6},
            new CategoryToBrand{id=7, CategoryId = 1 , BrandId=7},
            new CategoryToBrand{id=8, CategoryId = 1 , BrandId=8},
            new CategoryToBrand{id=9, CategoryId = 1 , BrandId=9},
            new CategoryToBrand{id=10, CategoryId = 1 , BrandId=10},

            new CategoryToBrand{id=11, CategoryId = 2 , BrandId=16},
            new CategoryToBrand{id=12, CategoryId = 2 , BrandId=17},
            new CategoryToBrand{id=13, CategoryId = 2 , BrandId=18},
            new CategoryToBrand{id=14, CategoryId = 2 , BrandId=19},
            new CategoryToBrand{id=15, CategoryId = 2 , BrandId=20},
            new CategoryToBrand{id=16, CategoryId = 2 , BrandId=21},
            new CategoryToBrand{id=17, CategoryId = 2 , BrandId=22},
            new CategoryToBrand{id=18, CategoryId = 2 , BrandId=23},
            new CategoryToBrand{id=19, CategoryId = 2 , BrandId=24},
            new CategoryToBrand{id=20, CategoryId = 2 , BrandId=25},

            new CategoryToBrand{id=21, CategoryId = 3 , BrandId=16},
            new CategoryToBrand{id=22, CategoryId = 3, BrandId=17},
            new CategoryToBrand{id=23, CategoryId = 3 , BrandId=18},
            new CategoryToBrand{id=24, CategoryId = 3 , BrandId=19},
            new CategoryToBrand{id=25, CategoryId = 3 , BrandId=20},
            new CategoryToBrand{id=26, CategoryId = 3 , BrandId=21},
            new CategoryToBrand{id=27, CategoryId = 3 , BrandId=22},
            new CategoryToBrand{id=28, CategoryId = 3 , BrandId=23},
            new CategoryToBrand{id=29, CategoryId = 3 , BrandId=24},
            new CategoryToBrand{id=30, CategoryId = 3 , BrandId=25},

            new CategoryToBrand{id=31, CategoryId =4 , BrandId=6},
            new CategoryToBrand{id=32, CategoryId = 4, BrandId=7},
            new CategoryToBrand{id=33, CategoryId = 4 , BrandId=8},
            new CategoryToBrand{id=34, CategoryId = 4 , BrandId=9},
            new CategoryToBrand{id=35, CategoryId = 4 , BrandId=10},
            new CategoryToBrand{id=36, CategoryId = 4 , BrandId=11},
            new CategoryToBrand{id=37, CategoryId = 4 , BrandId=12},
            new CategoryToBrand{id=38, CategoryId = 4 , BrandId=13},
            new CategoryToBrand{id=39, CategoryId = 4 , BrandId=14},
            new CategoryToBrand{id=40, CategoryId = 4 , BrandId=15},
            new CategoryToBrand{id=41, CategoryId = 4 , BrandId=16},
            new CategoryToBrand{id=42, CategoryId = 4, BrandId=17},
            new CategoryToBrand{id=43, CategoryId = 4 , BrandId=18},
            new CategoryToBrand{id=44, CategoryId = 4 , BrandId=19},
            new CategoryToBrand{id=45, CategoryId = 4 , BrandId=20},
            new CategoryToBrand{id=46, CategoryId = 4 , BrandId=21},
            new CategoryToBrand{id=47, CategoryId = 4 , BrandId=22},
            new CategoryToBrand{id=48, CategoryId = 4 , BrandId=23},
            new CategoryToBrand{id=49, CategoryId = 4 , BrandId=24},
            new CategoryToBrand{id=50, CategoryId = 4 , BrandId=25},             


            new CategoryToBrand{id=51, CategoryId = 6 , BrandId=6},
            new CategoryToBrand{id=52, CategoryId = 6 , BrandId=7},
            new CategoryToBrand{id=53, CategoryId = 6 , BrandId=8},
            new CategoryToBrand{id=54, CategoryId = 6 , BrandId=9},
            new CategoryToBrand{id=55, CategoryId = 6 , BrandId=10},
            new CategoryToBrand{id=56, CategoryId = 6 , BrandId=11},
            new CategoryToBrand{id=57, CategoryId = 6 , BrandId=21},
            new CategoryToBrand{id=58, CategoryId = 6 , BrandId=12},
            new CategoryToBrand{id=59, CategoryId = 6 , BrandId=14},
            new CategoryToBrand{id=60, CategoryId = 6 , BrandId=15},

            new CategoryToBrand{id=61, CategoryId = 7 , BrandId=16},
            new CategoryToBrand{id=62, CategoryId = 7 , BrandId=17},
            new CategoryToBrand{id=63, CategoryId = 7 , BrandId=18},
            new CategoryToBrand{id=64, CategoryId = 7 , BrandId=19},
            new CategoryToBrand{id=65, CategoryId = 7 , BrandId=20},
            new CategoryToBrand{id=66, CategoryId = 7 , BrandId=21},
            new CategoryToBrand{id=67, CategoryId = 7 , BrandId=22},
            new CategoryToBrand{id=68, CategoryId = 7 , BrandId=23},
            new CategoryToBrand{id=69, CategoryId = 7 , BrandId=24},
            new CategoryToBrand{id=70, CategoryId = 7 , BrandId=25},

            new CategoryToBrand{id=71, CategoryId = 8 , BrandId=16},
            new CategoryToBrand{id=72, CategoryId = 8 , BrandId=17},
            new CategoryToBrand{id=73, CategoryId = 8 , BrandId=18},
            new CategoryToBrand{id=74, CategoryId = 8 , BrandId=19},
            new CategoryToBrand{id=75, CategoryId = 8 , BrandId=20},
            new CategoryToBrand{id=76, CategoryId = 8 , BrandId=21},
            new CategoryToBrand{id=77, CategoryId = 8 , BrandId=22},
            new CategoryToBrand{id=78, CategoryId = 8 , BrandId=23},
            new CategoryToBrand{id=79, CategoryId = 8 , BrandId=24},
            new CategoryToBrand{id=80, CategoryId = 8 , BrandId=25},

            new CategoryToBrand{id=91, CategoryId =  9, BrandId=16},
            new CategoryToBrand{id=92, CategoryId = 9 , BrandId=17},
            new CategoryToBrand{id=93, CategoryId = 9 , BrandId=18},
            new CategoryToBrand{id=94, CategoryId = 9 , BrandId=19},
            new CategoryToBrand{id=95, CategoryId = 9 , BrandId=20},
            new CategoryToBrand{id=96, CategoryId = 9 , BrandId=21},
            new CategoryToBrand{id=97, CategoryId = 9 , BrandId=22},
            new CategoryToBrand{id=98, CategoryId = 9 , BrandId=23},
            new CategoryToBrand{id=99, CategoryId = 9 , BrandId=24},
            new CategoryToBrand{id=100, CategoryId = 9 , BrandId=25},

            new CategoryToBrand{id=101, CategoryId = 10 , BrandId=16},
            new CategoryToBrand{id=102, CategoryId = 10 , BrandId=17},
            new CategoryToBrand{id=103, CategoryId = 10 , BrandId=18},
            new CategoryToBrand{id=104, CategoryId = 10 , BrandId=19},
            new CategoryToBrand{id=105, CategoryId = 10 , BrandId=20},
            new CategoryToBrand{id=106, CategoryId = 10 , BrandId=21},
            new CategoryToBrand{id=107, CategoryId = 10 , BrandId=22},
            new CategoryToBrand{id=108, CategoryId = 10 , BrandId=23},
            new CategoryToBrand{id=109, CategoryId = 10 , BrandId=24},
            new CategoryToBrand{id=110, CategoryId =  10, BrandId=25},

            new CategoryToBrand{id=111, CategoryId = 11 , BrandId=16},
            new CategoryToBrand{id=112, CategoryId = 11 , BrandId=17},
            new CategoryToBrand{id=113, CategoryId = 11 , BrandId=18},
            new CategoryToBrand{id=114, CategoryId = 11 , BrandId=19},
            new CategoryToBrand{id=115, CategoryId = 11 , BrandId=20},
            new CategoryToBrand{id=116, CategoryId = 11 , BrandId=21},
            new CategoryToBrand{id=117, CategoryId = 11 , BrandId=22},
            new CategoryToBrand{id=118, CategoryId = 11 , BrandId=23},
            new CategoryToBrand{id=119, CategoryId = 11 , BrandId=24},
            new CategoryToBrand{id=120, CategoryId =  11, BrandId=25},

            new CategoryToBrand{id=121, CategoryId = 12 , BrandId=16},
            new CategoryToBrand{id=122, CategoryId = 12 , BrandId=17},
            new CategoryToBrand{id=123, CategoryId = 12 , BrandId=18},
            new CategoryToBrand{id=124, CategoryId = 12 , BrandId=19},
            new CategoryToBrand{id=125, CategoryId = 12 , BrandId=20},
            new CategoryToBrand{id=126, CategoryId = 12 , BrandId=21},
            new CategoryToBrand{id=127, CategoryId = 12 , BrandId=22},
            new CategoryToBrand{id=128, CategoryId = 12 , BrandId=23},
            new CategoryToBrand{id=129, CategoryId = 12 , BrandId=24},
            new CategoryToBrand{id=130, CategoryId =  12, BrandId=25},

            new CategoryToBrand{id=131, CategoryId = 13 , BrandId=16},
            new CategoryToBrand{id=132, CategoryId = 13, BrandId=17},
            new CategoryToBrand{id=133, CategoryId = 13 , BrandId=18},
            new CategoryToBrand{id=134, CategoryId = 13 , BrandId=19},
            new CategoryToBrand{id=135, CategoryId = 13 , BrandId=20},
            new CategoryToBrand{id=136, CategoryId = 13 , BrandId=21},
            new CategoryToBrand{id=137, CategoryId = 13 , BrandId=22},
            new CategoryToBrand{id=138, CategoryId = 13 , BrandId=23},
            new CategoryToBrand{id=139, CategoryId = 13 , BrandId=24},
            new CategoryToBrand{id=140, CategoryId =  13, BrandId=25},

            new CategoryToBrand{id=141, CategoryId = 5 , BrandId=16},
            new CategoryToBrand{id=142, CategoryId = 5 , BrandId=17},
            new CategoryToBrand{id=143, CategoryId = 5 , BrandId=18},
            new CategoryToBrand{id=144, CategoryId = 5 , BrandId=19},
            new CategoryToBrand{id=145, CategoryId = 5 , BrandId=20},
            new CategoryToBrand{id=146, CategoryId = 5 , BrandId=21},
            new CategoryToBrand{id=147, CategoryId = 5 , BrandId=22 },
            new CategoryToBrand{id=148, CategoryId = 5 , BrandId=23},
            new CategoryToBrand{id=149, CategoryId = 5 , BrandId=24},
            new CategoryToBrand{id=150, CategoryId = 5 , BrandId=25},
            #endregion

#region Категории спорта
            new CategoryToBrand{id=200, CategoryId =  14, BrandId=1},
            new CategoryToBrand{id=201, CategoryId =  14, BrandId=2},
            new CategoryToBrand{id=202, CategoryId =  14, BrandId=3},
            new CategoryToBrand{id=203, CategoryId =  14, BrandId=4},
            new CategoryToBrand{id=204, CategoryId =  14, BrandId=5},

            new CategoryToBrand{id=205, CategoryId =  15, BrandId=1},
            new CategoryToBrand{id=206, CategoryId =  15, BrandId=2},
            new CategoryToBrand{id=207, CategoryId =  15, BrandId=3},
            new CategoryToBrand{id=208, CategoryId =  15, BrandId=4},
            new CategoryToBrand{id=209, CategoryId =  15, BrandId=5},

            new CategoryToBrand{id=151, CategoryId =  16, BrandId=1},
            new CategoryToBrand{id=152, CategoryId =  16, BrandId=2},
            new CategoryToBrand{id=153, CategoryId =  16, BrandId=3},
            new CategoryToBrand{id=154, CategoryId =  16, BrandId=4},
            new CategoryToBrand{id=155, CategoryId =  16, BrandId=5},

            new CategoryToBrand{id=156, CategoryId =  17, BrandId=1},
            new CategoryToBrand{id=157, CategoryId =  17, BrandId=2},
            new CategoryToBrand{id=158, CategoryId =  17, BrandId=3},
            new CategoryToBrand{id=159, CategoryId =  17, BrandId=4},
            new CategoryToBrand{id=160, CategoryId =  17, BrandId=5},

            new CategoryToBrand{id=161, CategoryId =  18, BrandId=1},
            new CategoryToBrand{id=162, CategoryId =  18, BrandId=2},
            new CategoryToBrand{id=163, CategoryId =  18, BrandId=3},
            new CategoryToBrand{id=164, CategoryId =  18, BrandId=4},
            new CategoryToBrand{id=165, CategoryId =  18, BrandId=5},

            new CategoryToBrand{id=166, CategoryId =  19, BrandId=1},
            new CategoryToBrand{id=167, CategoryId =  19, BrandId=2},
            new CategoryToBrand{id=168, CategoryId =  19, BrandId=3},
            new CategoryToBrand{id=169, CategoryId =  19, BrandId=4},
            new CategoryToBrand{id=170, CategoryId =  19, BrandId=5},

            new CategoryToBrand{id=171, CategoryId =  20, BrandId=1},
            new CategoryToBrand{id=172, CategoryId =  20, BrandId=2},
            new CategoryToBrand{id=173, CategoryId =  20, BrandId=3},
            new CategoryToBrand{id=174, CategoryId =  20, BrandId=4},
            new CategoryToBrand{id=175, CategoryId =  20, BrandId=5},

            #endregion

#region Категории аксессуаров

            new CategoryToBrand{id=176, CategoryId =  21, BrandId=15},
            new CategoryToBrand{id=177, CategoryId =  21, BrandId=16},
            new CategoryToBrand{id=81, CategoryId =  21, BrandId=17},
            new CategoryToBrand{id=178, CategoryId =  21, BrandId=18},
            new CategoryToBrand{id=179, CategoryId =  21, BrandId=19},

            new CategoryToBrand{id=180, CategoryId =  22, BrandId=20},
            new CategoryToBrand{id=181, CategoryId =  22, BrandId=21},
            new CategoryToBrand{id=182, CategoryId =  22, BrandId=22},
            new CategoryToBrand{id=183, CategoryId =  22, BrandId=23},
            new CategoryToBrand{id=184, CategoryId =  22, BrandId=24},

            new CategoryToBrand{id=185, CategoryId =  23, BrandId=25},
            new CategoryToBrand{id=186, CategoryId =  23, BrandId=26},
            new CategoryToBrand{id=187, CategoryId =  23, BrandId=27},
            new CategoryToBrand{id=188, CategoryId =  23, BrandId=28},
            new CategoryToBrand{id=189, CategoryId =  23, BrandId=29},

            new CategoryToBrand{id=190, CategoryId =  24, BrandId=30},
            new CategoryToBrand{id=191, CategoryId =  24, BrandId=16},
            new CategoryToBrand{id=192, CategoryId =  24, BrandId=17},
            new CategoryToBrand{id=193, CategoryId =  24, BrandId=18},
            new CategoryToBrand{id=194, CategoryId =  24, BrandId=19},

            new CategoryToBrand{id=195, CategoryId =  25, BrandId=20},
            new CategoryToBrand{id=196, CategoryId =  25, BrandId=21},
            new CategoryToBrand{id=197, CategoryId =  25, BrandId=22},
            new CategoryToBrand{id=198, CategoryId =  25, BrandId=23},
            new CategoryToBrand{id=199, CategoryId =  25, BrandId=24}

           

#endregion
             //ПОСЛЕДНИЙ ID 209
        };


        public static List<SectionToBrands> SectionToBrands { get; }=new List<SectionToBrands>
        {
            new SectionToBrands{id=1,SectionId=1, BrandId=1},
            new SectionToBrands{id=2,SectionId=1, BrandId=2},
            new SectionToBrands{id=3,SectionId=1, BrandId=3},
            new SectionToBrands{id=4,SectionId=1, BrandId=4},
            new SectionToBrands{id=5,SectionId=1, BrandId=5},
            new SectionToBrands{id=6,SectionId=1, BrandId=6},
            new SectionToBrands{id=7,SectionId=1, BrandId=7},
            new SectionToBrands{id=8,SectionId=1, BrandId=8},
            new SectionToBrands{id=9,SectionId=1, BrandId=9},
            new SectionToBrands{id=10,SectionId=1, BrandId=10},
            new SectionToBrands{id=11,SectionId=1, BrandId=11},
            new SectionToBrands{id=12,SectionId=1, BrandId=12},
            new SectionToBrands{id=13,SectionId=1, BrandId=13},
            new SectionToBrands{id=14,SectionId=1, BrandId=14},
            new SectionToBrands{id=15,SectionId=1, BrandId=15},
            new SectionToBrands{id=16,SectionId=1, BrandId=16},
            new SectionToBrands{id=17,SectionId=1, BrandId=17},
            new SectionToBrands{id=18,SectionId=1, BrandId=18},
            new SectionToBrands{id=19,SectionId=1, BrandId=19},
            new SectionToBrands{id=20,SectionId=1, BrandId=20},
            new SectionToBrands{id=21,SectionId=1, BrandId=21},
            new SectionToBrands{id=22,SectionId=1, BrandId=22},
            new SectionToBrands{id=23,SectionId=1, BrandId=23},
            new SectionToBrands{id=24,SectionId=1, BrandId=24},
            new SectionToBrands{id=25,SectionId=1, BrandId=25},
            new SectionToBrands{id=26,SectionId=1, BrandId=26},
            new SectionToBrands{id=27,SectionId=1, BrandId=27},
            new SectionToBrands{id=28,SectionId=1, BrandId=28},
            new SectionToBrands{id=29,SectionId=1, BrandId=29},
            new SectionToBrands{id=30,SectionId=1, BrandId=30},

            new SectionToBrands{id=31,SectionId=2, BrandId=1},
            new SectionToBrands{id=32,SectionId=2, BrandId=2},
            new SectionToBrands{id=33,SectionId=2, BrandId=3},
            new SectionToBrands{id=34,SectionId=2, BrandId=4},
            new SectionToBrands{id=35,SectionId=2, BrandId=5},
            new SectionToBrands{id=36,SectionId=2, BrandId=6},
            new SectionToBrands{id=37,SectionId=2, BrandId=7},
            new SectionToBrands{id=38,SectionId=2, BrandId=8},
            new SectionToBrands{id=39,SectionId=2, BrandId=9},
            new SectionToBrands{id=40,SectionId=2, BrandId=10},
            new SectionToBrands{id=41,SectionId=2, BrandId=11},
            new SectionToBrands{id=42,SectionId=2, BrandId=12},
            new SectionToBrands{id=43,SectionId=2, BrandId=13},
            new SectionToBrands{id=44,SectionId=2, BrandId=14},
            new SectionToBrands{id=45,SectionId=2, BrandId=15},
            new SectionToBrands{id=46,SectionId=2, BrandId=16},
            new SectionToBrands{id=47,SectionId=2, BrandId=17},
            new SectionToBrands{id=48,SectionId=2, BrandId=18},
            new SectionToBrands{id=49,SectionId=2, BrandId=19},
            new SectionToBrands{id=50,SectionId=2, BrandId=20},
            new SectionToBrands{id=51,SectionId=2, BrandId=21},
            new SectionToBrands{id=52,SectionId=2, BrandId=22},
            new SectionToBrands{id=53,SectionId=2, BrandId=23},
            new SectionToBrands{id=54,SectionId=2, BrandId=24},
            new SectionToBrands{id=55,SectionId=2, BrandId=25},
            new SectionToBrands{id=56,SectionId=2, BrandId=26},
            new SectionToBrands{id=57,SectionId=2, BrandId=27},
            new SectionToBrands{id=58,SectionId=2, BrandId=28},
            new SectionToBrands{id=59,SectionId=2, BrandId=29},
            new SectionToBrands{id=60,SectionId=2, BrandId=30},

            new SectionToBrands{id=61,SectionId=3, BrandId=1},
            new SectionToBrands{id=62,SectionId=3, BrandId=2},
            new SectionToBrands{id=63,SectionId=3, BrandId=3},
            new SectionToBrands{id=64,SectionId=3, BrandId=4},
            new SectionToBrands{id=65,SectionId=3, BrandId=5},
            new SectionToBrands{id=66,SectionId=3, BrandId=6},
            new SectionToBrands{id=67,SectionId=3, BrandId=7},
            new SectionToBrands{id=68,SectionId=3, BrandId=8},
            new SectionToBrands{id=69,SectionId=3, BrandId=9},
            new SectionToBrands{id=70,SectionId=3, BrandId=10},
            new SectionToBrands{id=71,SectionId=3, BrandId=11},
            new SectionToBrands{id=72,SectionId=3, BrandId=12},
            new SectionToBrands{id=73,SectionId=3, BrandId=13},
            new SectionToBrands{id=74,SectionId=3, BrandId=14},
            new SectionToBrands{id=75,SectionId=3, BrandId=15},
            new SectionToBrands{id=76,SectionId=3, BrandId=16},
            new SectionToBrands{id=77,SectionId=3, BrandId=17},
            new SectionToBrands{id=78,SectionId=3, BrandId=18},
            new SectionToBrands{id=79,SectionId=3, BrandId=19},
            new SectionToBrands{id=80,SectionId=3, BrandId=20},
            new SectionToBrands{id=81,SectionId=3, BrandId=21},
            new SectionToBrands{id=82,SectionId=3, BrandId=22},
            new SectionToBrands{id=83,SectionId=3, BrandId=23},
            new SectionToBrands{id=84,SectionId=3, BrandId=24},
            new SectionToBrands{id=85,SectionId=3, BrandId=25},
            new SectionToBrands{id=86,SectionId=3, BrandId=26},
            new SectionToBrands{id=87,SectionId=3, BrandId=27},
            new SectionToBrands{id=88,SectionId=3, BrandId=28},
            new SectionToBrands{id=89,SectionId=3, BrandId=29},
            new SectionToBrands{id=90,SectionId=3, BrandId=30},

             new SectionToBrands{id=91,SectionId=4, BrandId=1},
            new SectionToBrands{id=92,SectionId=4, BrandId=2},
            new SectionToBrands{id=93,SectionId=4, BrandId=3},
            new SectionToBrands{id=94,SectionId=4, BrandId=4},
            new SectionToBrands{id=95,SectionId=4, BrandId=5},
            new SectionToBrands{id=96,SectionId=4, BrandId=6},
            new SectionToBrands{id=97,SectionId=4, BrandId=7},
            new SectionToBrands{id=98,SectionId=4, BrandId=8},
            new SectionToBrands{id=99,SectionId=4, BrandId=9},
            new SectionToBrands{id=100,SectionId=4, BrandId=10},
            new SectionToBrands{id=101,SectionId=4, BrandId=11},
            new SectionToBrands{id=102,SectionId=4, BrandId=12},
            new SectionToBrands{id=103,SectionId=4, BrandId=13},
            new SectionToBrands{id=104,SectionId=4, BrandId=14},
            new SectionToBrands{id=105,SectionId=4, BrandId=15},
            new SectionToBrands{id=106,SectionId=4, BrandId=16},
            new SectionToBrands{id=107,SectionId=4, BrandId=17},
            new SectionToBrands{id=108,SectionId=4, BrandId=18},
            new SectionToBrands{id=109,SectionId=4, BrandId=19},
            new SectionToBrands{id=110,SectionId=4, BrandId=20},
            new SectionToBrands{id=111,SectionId=4, BrandId=21},
            new SectionToBrands{id=112,SectionId=4, BrandId=22},
            new SectionToBrands{id=113,SectionId=4, BrandId=23},
            new SectionToBrands{id=114,SectionId=4, BrandId=24},
            new SectionToBrands{id=115,SectionId=4, BrandId=25},
            new SectionToBrands{id=116,SectionId=4, BrandId=26},
            new SectionToBrands{id=117,SectionId=4, BrandId=27},
            new SectionToBrands{id=118,SectionId=4, BrandId=28},
            new SectionToBrands{id=119,SectionId=4, BrandId=29},
            new SectionToBrands{id=120,SectionId=4, BrandId=30}
        };

        public static List<FileModel> Files { get; } = new List<FileModel>
        {
            new FileModel{id=1, Name="Photo" ,Path="photo.jpeg"}
        };

        /// <summary>
        /// Тестовый набор Товаров
        /// </summary>
        public static List<Product> Products { get; } = new List<Product>
        {
            new Product { id = 1, Name = "Футболка", Price = 100, FileId = 1, Order = 0, SectionId = 1, BrandId = 6, CategoryId = 6},
            new Product { id = 2, Name = "Футболка", Price = 200, FileId = 1, Order = 1, SectionId = 1, BrandId = 7 , CategoryId = 6},
            new Product { id = 3, Name = "Футболка", Price = 300, FileId = 1, Order = 2, SectionId = 1, BrandId = 8 , CategoryId = 6},
            new Product { id = 4, Name = "Футболка", Price = 400, FileId = 1, Order = 3, SectionId = 1, BrandId = 9 , CategoryId = 6},
            new Product { id = 5, Name = "Футболка", Price = 500, FileId = 1, Order = 4, SectionId = 1, BrandId = 10 , CategoryId = 6},
            new Product { id = 6, Name = "Футболка", Price = 600, FileId = 1, Order = 5, SectionId = 1, BrandId = 11 , CategoryId = 6},
            new Product { id = 7, Name = "Футболка", Price = 700, FileId = 1, Order = 6, SectionId = 1, BrandId = 21, CategoryId = 6},
            new Product { id = 8, Name = "Футболка", Price = 800, FileId = 1, Order = 7, SectionId = 1, BrandId = 12 , CategoryId = 6},
            new Product { id = 9, Name = "Футболка", Price = 900, FileId = 1, Order = 8, SectionId = 1, BrandId = 13 , CategoryId = 6},
            new Product { id = 10, Name = "Футболка", Price = 1000, FileId =1, Order = 9, SectionId = 1, BrandId = 14 , CategoryId = 6},
            new Product { id = 11, Name = "Футболка", Price = 1100, FileId = 1, Order = 10, SectionId = 1, BrandId = 15 , CategoryId = 6},
            new Product { id = 12, Name = "Футболка", Price = 1200, FileId = 1, Order = 11, SectionId = 1, BrandId = 6, CategoryId = 6},

            new Product { id = 13, Name = "Футболка", Price = 100, FileId = 1, Order = 0, SectionId = 2, BrandId = 6, CategoryId = 6},
            new Product { id = 14, Name = "Футболка", Price = 200, FileId = 1, Order = 1, SectionId = 2, BrandId = 7 , CategoryId = 6},
            new Product { id = 15, Name = "Футболка", Price = 300, FileId = 1, Order = 2, SectionId = 2, BrandId = 8 , CategoryId = 6},
            new Product { id = 16, Name = "Футболка", Price = 400, FileId = 1, Order = 3, SectionId = 2, BrandId = 9 , CategoryId = 6},
            new Product { id = 17, Name = "Футболка", Price = 500, FileId = 1, Order = 4, SectionId = 2, BrandId = 10 , CategoryId = 6},
            new Product { id = 18, Name = "Футболка", Price = 600, FileId = 1, Order = 5, SectionId = 2, BrandId = 11 , CategoryId = 6},
            new Product { id = 19, Name = "Футболка", Price = 700, FileId = 1, Order = 6, SectionId = 2, BrandId = 21, CategoryId = 6},
            new Product { id = 48, Name = "Футболка", Price = 800, FileId = 1, Order = 7, SectionId = 2, BrandId = 12 , CategoryId = 6},
            new Product { id = 20, Name = "Футболка", Price = 900, FileId = 1, Order = 8, SectionId = 2, BrandId = 13 , CategoryId = 6},
            new Product { id = 21, Name = "Футболка", Price = 1000, FileId =1, Order = 9, SectionId = 2, BrandId = 14 , CategoryId = 6},
            new Product { id = 22, Name = "Футболка", Price = 1100, FileId = 1, Order = 10, SectionId = 2, BrandId = 15 , CategoryId = 6},
            new Product { id = 23, Name = "Футболка", Price = 1200, FileId = 1, Order = 11, SectionId = 2, BrandId = 6, CategoryId = 6},

            new Product { id = 24, Name = "Джинсы", Price = 100, FileId = 1, Order = 0, SectionId = 2, BrandId = 16, CategoryId = 5},
            new Product { id = 25, Name = "Джинсы", Price = 200, FileId = 1, Order = 1, SectionId = 2, BrandId = 17 , CategoryId = 5},
            new Product { id = 26, Name = "Джинсы", Price = 300, FileId = 1, Order = 2, SectionId = 2, BrandId = 18 , CategoryId = 5},
            new Product { id = 27, Name = "Джинсы", Price = 400, FileId = 1, Order = 3, SectionId = 2, BrandId = 19 , CategoryId = 5},
            new Product { id = 28, Name = "Джинсы", Price = 500, FileId = 1, Order = 4, SectionId = 2, BrandId = 20 , CategoryId =5},
            new Product { id = 29, Name = "Джинсы", Price = 600, FileId = 1, Order = 5, SectionId = 2, BrandId = 21 , CategoryId = 5},
            new Product { id = 30, Name = "Джинсы", Price = 700, FileId = 1, Order = 6, SectionId = 2, BrandId = 22, CategoryId = 5},
            new Product { id = 31, Name = "Джинсы", Price = 800, FileId = 1, Order = 7, SectionId = 2, BrandId = 23, CategoryId = 5},
            new Product { id = 32, Name = "Джинсы", Price = 900, FileId = 1, Order = 8, SectionId = 2, BrandId = 24 , CategoryId = 5},
            new Product { id = 33, Name = "Джинсы", Price = 1000, FileId =1, Order = 9, SectionId = 2, BrandId = 25 , CategoryId = 5},
            new Product { id = 34, Name = "Джинсы", Price = 1100, FileId = 1, Order = 10, SectionId = 2, BrandId = 16 , CategoryId = 5},
            new Product { id = 35, Name = "Джинсы", Price = 1200, FileId = 1, Order = 11, SectionId = 2, BrandId = 16 , CategoryId = 5},

             new Product { id = 36, Name = "Джинсы", Price = 100, FileId = 1, Order = 0, SectionId = 1, BrandId = 16, CategoryId = 5},
            new Product { id = 37, Name = "Джинсы", Price = 200, FileId = 1, Order = 1, SectionId = 1, BrandId = 17 , CategoryId = 5},
            new Product { id = 38, Name = "Джинсы", Price = 300, FileId = 1, Order = 2, SectionId = 1, BrandId = 18 , CategoryId = 5},
            new Product { id = 39, Name = "Джинсы", Price = 400, FileId = 1, Order = 3, SectionId = 1, BrandId = 19 , CategoryId = 5},
            new Product { id = 40, Name = "Джинсы", Price = 500, FileId = 1, Order = 4, SectionId = 1, BrandId = 20 , CategoryId =5},
            new Product { id = 41, Name = "Джинсы", Price = 600, FileId = 1, Order = 5, SectionId = 1, BrandId = 21 , CategoryId = 5},
            new Product { id = 42, Name = "Джинсы", Price = 700, FileId = 1, Order = 6, SectionId = 1, BrandId = 22, CategoryId = 5},
            new Product { id = 43, Name = "Джинсы", Price = 800, FileId = 1, Order = 7, SectionId = 1, BrandId = 23, CategoryId = 5},
            new Product { id = 44, Name = "Джинсы", Price = 900, FileId = 1, Order = 8, SectionId = 1, BrandId = 24 , CategoryId = 5},
            new Product { id = 45, Name = "Джинсы", Price = 1000, FileId =1, Order = 9, SectionId = 1, BrandId = 25 , CategoryId = 5},
            new Product { id = 46, Name = "Джинсы", Price = 1100, FileId = 1, Order = 10, SectionId = 1, BrandId = 16 , CategoryId = 5},
            new Product { id = 47, Name = "Джинсы", Price = 1200, FileId = 1, Order = 11, SectionId = 1, BrandId = 16 , CategoryId = 5},
        };
    

        /// <summary>
        /// Тестовый набор Сотрудников
        /// </summary>
        public static List<Employee> Employees { get; } = new List<Employee>
        {
            new Employee{id = 1, Name = "Сергей", Surname ="Зайковский", Patronimic = "Сергеевич" ,Age= 30, Email="asd@mail.ru",PositionId = 5},
            new Employee{id = 2, Name = "Иван", Surname ="Петров",  Patronimic = "Иванович" ,Age= 25, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 3, Name = "Петр", Surname ="Иванов",  Patronimic = "петрович" ,Age= 45, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 4, Name = "Семен", Surname ="Семенов", Patronimic = "Семенович" , Age= 35, Email="asd@mail.ru",PositionId = 2},
            new Employee{id = 5, Name = "Евгений", Surname ="Федоров", Patronimic = "Владиславович" , Age= 35, Email="asd@mail.ru",PositionId =1},
            new Employee{id = 6, Name = "Сергей", Surname ="Сидоров", Patronimic = "Сергеевич" ,Age= 35, Email="asd@mail.ru",PositionId = 1 },
            new Employee{id = 7, Name = "Иван", Surname ="Иванук",  Patronimic = "Иванович" ,Age= 25, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 8, Name = "Петр", Surname ="Бадоев",  Patronimic = "петрович" ,Age= 45, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 9, Name = "Семен", Surname ="Блохин", Patronimic = "Семенович" , Age= 35, Email="asd@mail.ru",PositionId = 2},
            new Employee{id = 10, Name = "Евгений", Surname ="Федотов", Patronimic = "Владиславович" , Age= 35, Email="asd@mail.ru",PositionId =1},
            new Employee{id = 11, Name = "Сергей", Surname ="Кременчук", Patronimic = "Сергеевич" ,Age= 35, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 12, Name = "Иван", Surname ="Самсонов",  Patronimic = "Иванович" ,Age= 25, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 13, Name = "Петр", Surname ="Пиганович",  Patronimic = "петрович" ,Age= 45, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 14, Name = "Семен", Surname ="Петровичкий", Patronimic = "Семенович" , Age= 35, Email="asd@mail.ru",PositionId =2},
            new Employee{id = 15, Name = "Евгений", Surname ="Чайковский", Patronimic = "Владиславович" , Age= 35, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 16, Name = "Сергей", Surname ="Цветоков", Patronimic = "Сергеевич" ,Age= 35, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 17, Name = "Иван", Surname ="Петров",  Patronimic = "Иванович" ,Age= 25, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 18, Name = "Петр", Surname ="Иванов",  Patronimic = "петрович" ,Age= 45, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 19, Name = "Семен", Surname ="Семенов", Patronimic = "Семенович" , Age= 35, Email="asd@mail.ru",PositionId = 2},
            new Employee{id = 20, Name = "Евгений", Surname ="Федоров", Patronimic = "Владиславович" , Age= 35, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 21, Name = "Сергей", Surname ="Зайковский", Patronimic = "Сергеевич" ,Age= 35, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 22, Name = "Иван", Surname ="Петров",  Patronimic = "Иванович" ,Age= 25, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 23, Name = "Петр", Surname ="Иванов",  Patronimic = "петрович" ,Age= 45, Email="asd@mail.ru",PositionId = 1},
            new Employee{id = 24, Name = "Семен", Surname ="Семенов", Patronimic = "Семенович" , Age= 35, Email="asd@mail.ru",PositionId = 2},
            new Employee{id = 25, Name = "Евгений", Surname ="Федоров", Patronimic = "Владиславович" , Age= 35, Email="asd@mail.ru",PositionId = 3},
             new Employee{id = 26, Name = "Семен", Surname ="Семенов", Patronimic = "Семенович" , Age= 35, Email="asd@mail.ru",PositionId = 4},
            new Employee{id = 27, Name = "Евгений", Surname ="Федоров", Patronimic = "Владиславович" , Age= 35, Email="asd@mail.ru",PositionId = 5}
        };

        /// <summary>
        /// Тестовый набор Должностей
        /// </summary>
        public static List<Position> Positions { get; } = new List<Position>
        {
                new Position{id=1, Name="Менеджер по продажам"},
                new Position{id=2, Name="Ведущий менеджер"},
                new Position{id=3, Name="Бухгалтер"},
                new Position{id=4, Name="Системный администратор"},
                new Position{id=5, Name="Генеральный директор"}
        };       
    }
}

using OnlineStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Areas.Admin.ViewModels
{

    /// <summary>
    /// View модель сортировки товаров, добавляется к модели товаров
    /// </summary>

    public class SortViewModelForEmployees
    {
        public EnumSortForEmployee NameSort { get; set; }
        public EnumSortForEmployee SurnameSort { get; set; }
        public EnumSortForEmployee PatronimicSort { get; set; }
        public EnumSortForEmployee AgeSort { get; set; }
        public EnumSortForEmployee PositionSort { get; set; }
        public EnumSortForEmployee Current { get; set; }

        public bool Up { get; set; }

        public SortViewModelForEmployees(EnumSortForEmployee SortEnum)
        {
            NameSort = EnumSortForEmployee.NameAsc;
            SurnameSort = EnumSortForEmployee.SurnameAsc;
            PatronimicSort = EnumSortForEmployee.PatronimicAsc;
            AgeSort = EnumSortForEmployee.AgeAsc;
            PositionSort = EnumSortForEmployee.PosAsc;

            Up = true;

            if (SortEnum == EnumSortForEmployee.AgeDes || SortEnum == EnumSortForEmployee.NameDes
                || SortEnum == EnumSortForEmployee.SurnameDes
                || SortEnum == EnumSortForEmployee.PatronimicDes
                || SortEnum == EnumSortForEmployee.PosDes)
            {
                Up = false;
            }

            switch (SortEnum)
            {
                case EnumSortForEmployee.SurnameDes:
                    Current = SurnameSort = EnumSortForEmployee.SurnameAsc;
                    break;

                case EnumSortForEmployee.NameAsc:
                    Current = NameSort = EnumSortForEmployee.NameDes;
                    break;
                case EnumSortForEmployee.NameDes:
                    Current = NameSort = EnumSortForEmployee.NameAsc;
                    break;

                case EnumSortForEmployee.PatronimicAsc:
                    Current = PatronimicSort = EnumSortForEmployee.PatronimicDes;
                    break;
                case EnumSortForEmployee.PatronimicDes:
                    Current = PatronimicSort = EnumSortForEmployee.PatronimicAsc;
                    break;

                case EnumSortForEmployee.AgeAsc:
                    Current = AgeSort = EnumSortForEmployee.AgeDes;
                    break;
                case EnumSortForEmployee.AgeDes:
                    Current = AgeSort = EnumSortForEmployee.AgeAsc;
                    break;

                case EnumSortForEmployee.PosAsc:
                    Current = PositionSort = EnumSortForEmployee.PosDes;
                    break;
                case EnumSortForEmployee.PosDes:
                    Current = PositionSort = EnumSortForEmployee.PosAsc;
                    break;

                default:
                    Current = SurnameSort = EnumSortForEmployee.SurnameDes;
                    break;
            }
        }

    }

}



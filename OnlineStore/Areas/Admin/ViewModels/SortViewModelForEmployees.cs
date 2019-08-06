using OnlineStore.Domain.SortsEntities;
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
        public string NameSort { get; set; }
        public string SurnameSort { get; set; }
        public string PatronimicSort { get; set; }
        public string AgeSort { get; set; }
        public string PositionSort { get; set; }
        public string Current { get; set; }

        public bool Up { get; set; }

        public SortViewModelForEmployees(string SortType)
        {
            NameSort = SortEntityForEmployee.NameAsc;
            SurnameSort = SortEntityForEmployee.SurnameAsc;
            PatronimicSort = SortEntityForEmployee.PatronimicAsc;
            AgeSort = SortEntityForEmployee.AgeAsc;
            PositionSort = SortEntityForEmployee.PosAsc;

            Up = true;

            if (SortType == SortEntityForEmployee.AgeDes || SortType == SortEntityForEmployee.NameDes
                || SortType == SortEntityForEmployee.SurnameDes
                || SortType == SortEntityForEmployee.PatronimicDes
                || SortType == SortEntityForEmployee.PosDes)
            {
                Up = false;
            }

            switch (SortType)
            {
                case SortEntityForEmployee.SurnameDes:
                    Current = SurnameSort = SortEntityForEmployee.SurnameAsc;
                    break;

                case SortEntityForEmployee.NameAsc:
                    Current = NameSort = SortEntityForEmployee.NameDes;
                    break;
                case SortEntityForEmployee.NameDes:
                    Current = NameSort = SortEntityForEmployee.NameAsc;
                    break;

                case SortEntityForEmployee.PatronimicAsc:
                    Current = PatronimicSort = SortEntityForEmployee.PatronimicDes;
                    break;
                case SortEntityForEmployee.PatronimicDes:
                    Current = PatronimicSort = SortEntityForEmployee.PatronimicAsc;
                    break;

                case SortEntityForEmployee.AgeAsc:
                    Current = AgeSort = SortEntityForEmployee.AgeDes;
                    break;
                case SortEntityForEmployee.AgeDes:
                    Current = AgeSort = SortEntityForEmployee.AgeAsc;
                    break;

                case SortEntityForEmployee.PosAsc:
                    Current = PositionSort = SortEntityForEmployee.PosDes;
                    break;
                case SortEntityForEmployee.PosDes:
                    Current = PositionSort = SortEntityForEmployee.PosAsc;
                    break;

                default:
                    Current = SurnameSort = SortEntityForEmployee.SurnameDes;
                    break;
            }
        }

    }

}



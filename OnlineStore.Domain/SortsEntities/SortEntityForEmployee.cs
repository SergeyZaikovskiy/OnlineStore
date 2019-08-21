using OnlineStore.Domain.SortsEntities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.SortsEntities
{
    public class SortEntityForEmployee : BaseSortEntity
    {
        public const string SurnameAsc = "SurnameAsc";
        public const string SurnameDes = "SurnameDes";

        public const string PatronimicAsc = "PatronimicAsc";
        public const string PatronimicDes = "PatronimicDes";

        public const string AgeAsc = "AgeAsc";
        public const string AgeDes = "AgeDes";

        public const string PosAsc = "PosAsc";
        public const string PosDes = "PosDes";       
    }
}

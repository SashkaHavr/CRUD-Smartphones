using DAL;
using System.Collections.Generic;

namespace CRUD_proj.Models.Smartphone_IComparers
{
    class ManufacturerComparer : IComparer<Smartphone>
    {
        public int Compare(Smartphone x, Smartphone y) => x.Manufacturer.CompareTo(y.Manufacturer);
    }
}

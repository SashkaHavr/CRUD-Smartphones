using DAL;
using System.Collections.Generic;

namespace CRUD_proj.Models.Smartphone_IComparers
{
    class PriceComparer : IComparer<Smartphone>
    {
        public int Compare(Smartphone x, Smartphone y) => double.Parse(x.Price).CompareTo(double.Parse(y.Price));
    }
}

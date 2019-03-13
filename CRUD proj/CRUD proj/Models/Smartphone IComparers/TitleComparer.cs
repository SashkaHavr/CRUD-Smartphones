using DAL;
using System.Collections.Generic;

namespace CRUD_proj.Models.Smartphone_IComparers
{
    class TitleComparer : IComparer<Smartphone>
    {
        public int Compare(Smartphone x, Smartphone y) => x.Title.CompareTo(y.Title);
    }
}

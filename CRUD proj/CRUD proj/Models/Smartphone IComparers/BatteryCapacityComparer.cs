using System.Collections.Generic;

namespace CRUD_proj.Models.Smartphone_IComparers
{
    class BatteryCapacityComparer : IComparer<Smartphone>
    {
        public int Compare(Smartphone x, Smartphone y) => x.BatteryCapacity.CompareTo(y.BatteryCapacity);
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace CRUD_proj.Models
{
    static class Extention
    {

        public static void Sort<T>(this ObservableCollection<T> collection, IComparer<T> comparer)
        {
            for (int i = 0; i < collection.Count; i++)
                for (int j = 0; j < collection.Count; j++)
                    if (comparer.Compare(collection[i], collection[j]) < 0)
                    {
                        T t = collection[i];
                        collection[i] = collection[j];
                        collection[j] = t;
                    }
        }

        public static void StaticReverse<T>(this ObservableCollection<T> collection)
        {
            for(int i = 0; i<collection.Count / 2; i++)
            {
                T t = collection[i];
                collection[i] = collection[collection.Count - i - 1];
                collection[collection.Count - i - 1] = t;
            }
        }

    }
}

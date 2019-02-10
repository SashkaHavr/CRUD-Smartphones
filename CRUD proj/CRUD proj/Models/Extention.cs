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

        public static string LeftPathWithoutCurPath(this string path)
        {
            if (path.Contains(Environment.CurrentDirectory))
                return path.Replace(Environment.CurrentDirectory + '\\', string.Empty);
            return path;
        }

        public static string SetPathWithoutRoot(this string path)
        {
            string ext = Path.GetExtension(path);
            if (ext == ".png" || ext == ".jpg" || ext == ".bmp" || ext == ".gif")
                if (!Uri.IsWellFormedUriString(path, UriKind.Absolute) && !Path.IsPathRooted(path))
                    return Path.Combine(Environment.CurrentDirectory, path);
            return path;
        }

    }
}

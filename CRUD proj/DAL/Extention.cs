using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    static class Extention
    {
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

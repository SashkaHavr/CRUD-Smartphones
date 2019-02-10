using System;
using System.Threading;

namespace CRUD_proj.Models.Structs
{
    struct DoubleString : IComparable<DoubleString>
    {
        string comaValue;
        double dValue;

        public string Value
        {
            get => comaValue != string.Empty ? comaValue : dValue.ToString();
            set
            {
                if (double.TryParse(value, out double number))
                {
                    if (value[value.Length - 1].ToString() == Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                        comaValue = value;
                    else
                    {
                        comaValue = string.Empty;
                        dValue = number;
                    }
                }
            }
        }

        public int CompareTo(DoubleString other) => dValue.CompareTo(other.dValue);

        public static bool operator <(DoubleString x, DoubleString y) => x.dValue < y.dValue;
        public static bool operator >(DoubleString x, DoubleString y) => x.dValue > y.dValue;
        public static bool operator <=(DoubleString x, DoubleString y) => x.dValue <= y.dValue;
        public static bool operator >=(DoubleString x, DoubleString y) => x.dValue >= y.dValue;
        public static bool operator <(DoubleString x, string y) => x.dValue < double.Parse(y);
        public static bool operator >(DoubleString x, string y) => x.dValue > double.Parse(y);
        public static bool operator <=(DoubleString x, string y) => x.dValue <= double.Parse(y);
        public static bool operator >=(DoubleString x, string y) => x.dValue >= double.Parse(y);
        public static bool operator <(string y, DoubleString x) => x > y;
        public static bool operator >(string y, DoubleString x) => x < y;
        public static bool operator <=(string y, DoubleString x) => x >= y;
        public static bool operator >=(string y, DoubleString x) => x <= y;
    }
}
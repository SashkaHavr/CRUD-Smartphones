using DAL;
namespace CRUD_proj.Models.Structs
{
    struct FromToDoubleString
    {
        DoubleString from;
        DoubleString to;
        DoubleString minFrom;
        DoubleString maxTo;

        public string From
        {
            get => from.Value;
            set
            {
                if (to >= minFrom && to < value)
                    from.Value = to.Value;
                else if (value <= maxTo)
                {
                    if (value >= minFrom)
                        from.Value = value;
                    else
                        from.Value = minFrom.Value;
                }
                else
                    from.Value = maxTo.Value;

            }
        }

        public string To
        {
            get => to.Value;
            set
            {
                if (from <= maxTo && from > value)
                    to.Value = from.Value;
                else if (value >= minFrom)
                {
                    if (value <= maxTo)
                        to.Value = value;
                    else
                        to.Value = maxTo.Value;
                }
                else
                    to.Value = minFrom.Value;

            }
        }

        public string MinFrom
        {
            get => minFrom.Value;
            set
            {
                minFrom.Value = value;
                    from.Value = value;
            }       
        }
        public string MaxTo
        {
            get => maxTo.Value;
            set
            {
                maxTo.Value = value;
                    to.Value = value;
            }
        }

        public void Reset()
        {
            from.Value = minFrom.Value;
            to.Value = maxTo.Value;
        }
    }
}

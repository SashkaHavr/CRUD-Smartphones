namespace CRUD_proj.Models.Structs
{
    struct FromToInteger
    {
        int from;
        int to;
        int minFrom;
        int maxTo;
        public bool IsMinNotNull { get; set; }

        public int From
        {
            get => from;
            set
            {
                if (to >= minFrom && to < value)
                    from = to;
                else if (value <= maxTo)
                {
                    if (value >= minFrom)
                        from = value;
                    else
                        from = minFrom;
                }
                else
                    from = maxTo;
            }
        }

        public int To
        {
            get => to;
            set
            {
                if (from <= maxTo && from > value)
                    to = from;
                else if (value >= minFrom)
                {
                    if (value <= maxTo)
                        to = value;
                    else
                        to = maxTo;
                }
                else
                    to = minFrom;
            }
        }

        public int MinFrom
        {
            get => minFrom;
            set
            {
                minFrom = value;
                    from = value;
            }
        }
        public int MaxTo
        {
            get => maxTo;
            set
            {
                maxTo = value;
                    to = value;
            }
        }

        public void Reset()
        {
            from = minFrom;
            to = maxTo;
        }
    }
}

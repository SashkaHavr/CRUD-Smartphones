namespace CRUD_proj.Models
{
    class Resolution : NotifiedModelBase
    {
        int horizontal;
        int vertical;

        public int Horizontal { get => horizontal; set { horizontal = value; Notify(); } }
        public int Vertical { get => vertical; set { vertical = value; Notify(); } }

        public override string ToString() => horizontal == 0 && vertical == 0 ? string.Empty : $"{horizontal}x{vertical}";
    }
}

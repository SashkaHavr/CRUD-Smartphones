using CRUD_proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD_proj.Views.UserControls
{
    /// <summary>
    /// Interaction logic for FromToTextBox.xaml
    /// </summary>
    public partial class FromToTextBox : UserControl
    {
        public FromToTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(FromToTextBox));
        public static readonly DependencyProperty FromHintProperty = DependencyProperty.Register("FromHint", typeof(string), typeof(FromToTextBox));
        public static readonly DependencyProperty FromTextProperty = DependencyProperty.Register("FromText", typeof(string), typeof(FromToTextBox));
        public static readonly DependencyProperty ToHintProperty = DependencyProperty.Register("ToHint", typeof(string), typeof(FromToTextBox));
        public static readonly DependencyProperty ToTextProperty = DependencyProperty.Register("ToText", typeof(string), typeof(FromToTextBox));


        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }

        public string FromHint
        {
            get => GetValue(FromHintProperty) as string;
            set => SetValue(FromHintProperty, value);
        }

        public string FromText
        {
            get => GetValue(FromTextProperty) as string;
            set => SetValue(FromTextProperty, value);
        }

        public string ToHint
        {
            get => GetValue(ToHintProperty) as string;
            set => SetValue(ToHintProperty, value);
        }

        public string ToText
        {
            get => GetValue(ToTextProperty) as string;
            set => SetValue(ToTextProperty, value);
        }
    }
}

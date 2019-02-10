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
    /// Interaction logic for TitledTextBlock.xaml
    /// </summary>
    public partial class TitledTextBlock : UserControl
    {
        public TitledTextBlock()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TitledTextBlock));
        public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register("TextContent", typeof(string), typeof(TitledTextBlock));

        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }
            
        public string TextContent
        {
            get => GetValue(TextContentProperty) as string;
            set => SetValue(TextContentProperty, value);
        }
    }
}

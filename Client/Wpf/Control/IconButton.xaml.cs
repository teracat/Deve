using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FontAwesome6;

namespace Deve.ClientApp.Wpf.Control
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class IconButton : Button
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(EFontAwesomeIcon), typeof(DataListControl), new FrameworkPropertyMetadata(EFontAwesomeIcon.Solid_Question));
        public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register(nameof(IconColor), typeof(Brush), typeof(DataListControl), new FrameworkPropertyMetadata(Brushes.Black));

        public EFontAwesomeIcon Icon
        {
            get => (EFontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public Brush IconColor
        {
            get => (Brush)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public IconButton()
        {
            InitializeComponent();
        }
    }
}

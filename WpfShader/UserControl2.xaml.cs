using System.Windows;
using System.Windows.Controls;
using WpfShader.ViewModel;

namespace WpfShader
{
    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        private GrayScale _effect = new GrayScale();
        public UserControl2()
        {
            InitializeComponent();
            TestGrid.Effect = _effect;
            GrayAmountSlider.ValueChanged += GrayAmountSlider_ValueChanged;
        }

        private void GrayAmountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _effect.Amount = GrayAmountSlider.Value;
        }
    }
}

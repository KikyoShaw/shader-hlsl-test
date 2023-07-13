using System.Windows.Controls;
using WpfShader.ViewModel;

namespace WpfShader
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private GrayFilter _effect = new GrayFilter();
        public UserControl1()
        {
            InitializeComponent();
            TestGrid.Effect = _effect;
        }
    }
}

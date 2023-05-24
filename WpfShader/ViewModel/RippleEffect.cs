using System;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows;

namespace WpfShader.ViewModel
{
    public class RippleEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(RippleEffect), 0);

        public static readonly DependencyProperty MousePositionProperty = DependencyProperty.Register("MousePosition", typeof(Point), typeof(RippleEffect), new UIPropertyMetadata(new Point(0, 0), PixelShaderConstantCallback(0)));

        public static readonly DependencyProperty DecayProperty = DependencyProperty.Register("Decay", typeof(double), typeof(RippleEffect), new UIPropertyMetadata(0.9, PixelShaderConstantCallback(1)));

        public static readonly DependencyProperty DampingProperty = DependencyProperty.Register("Damping", typeof(double), typeof(RippleEffect), new UIPropertyMetadata(0.9, PixelShaderConstantCallback(2)));

        public RippleEffect()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri("pack://application:,,,/WpfShader;Component/effect/Ripple.ps");
            PixelShader = pixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(MousePositionProperty);
            UpdateShaderValue(DecayProperty);
            UpdateShaderValue(DampingProperty);
        }

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public Point MousePosition
        {
            get { return (Point)GetValue(MousePositionProperty); }
            set { SetValue(MousePositionProperty, value); }
        }

        public double Decay
        {
            get { return (double)GetValue(DecayProperty); }
            set { SetValue(DecayProperty, value); }
        }

        public double Damping
        {
            get { return (double)GetValue(DampingProperty); }
            set { SetValue(DampingProperty, value); }
        }
    }
}

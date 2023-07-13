using System;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows;

namespace WpfShader.ViewModel
{
    public class GrayScale : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(GrayScale), 0);
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(double), typeof(GrayScale),
                new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

        public GrayScale()
        {
            Init();
        }

        public void Init()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri(@"/WpfShader;Component/effect/GrayScale.ps", UriKind.Relative);
            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(AmountProperty);
        }

        public Brush Input
        {
            get => ((Brush)(this.GetValue(InputProperty)));
            set => this.SetValue(InputProperty, value);
        }

        public double Amount
        {
            get => ((double)(this.GetValue(AmountProperty)));
            set => this.SetValue(AmountProperty, value);
        }   
    }
}

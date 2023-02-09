using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Effects
{
    public class GreyEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(GreyEffect), 0);

        public static readonly DependencyProperty DeepProperty = DependencyProperty.Register("Deep", typeof(double), typeof(GreyEffect), new UIPropertyMetadata(0D, PixelShaderConstantCallback(0)));

        /// <summary>
        /// The uri should be something like pack://application:,,,/Gu.Wpf.Geometry;component/Effects/GreyEffect.ps
        /// The file GreyEffect.ps should have BuildAction: Resource
        /// </summary>
        private static readonly PixelShader Shader = new PixelShader
        {
            UriSource = new Uri("pack://application:,,,/LeeTeke.WpfControl;component/LeeTekeDatas/GreyEffect.ps", UriKind.Absolute)
        };

        public GreyEffect()
        {
            this.PixelShader = Shader;
            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(DeepProperty);
        }

        /// <summary>
        /// There has to be a property of type Brush called "Input". This property contains the input image and it is usually not set directly - it is set automatically when our effect is applied to a control.
        /// </summary>
        public Brush Input
        {
            get
            {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }

        public double Deep
        {
            get
            {
                return ((double)(this.GetValue(DeepProperty)));
            }
            set
            {
                this.SetValue(DeepProperty, value);
            }
        }
    }
}

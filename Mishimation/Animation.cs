using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;

namespace Mishimation
{
    public class Animation : AnimationBase
    {
        Func<double, float, float, double, float> easing;
        private double initValue, endValue;
        AnimatableProperty property;

        public Animation(double init, double end, AnimatableProperty prop, double duration, Func<double, float, float, double, float> easing) : base(duration)
        {
            this.initValue = init;
            this.endValue = end;
            this.property = prop;
            this.easing = easing;
        }

        public override void Reset()
        {
            ResetTime();
            this.property.Set(initValue);
        }

        public override void Update(double timestep, double duration)
        {
            double currentValue = (double)easing(timestep, (float)initValue, (float)endValue, duration);
            this.property.Set(currentValue);
        }
    }
}

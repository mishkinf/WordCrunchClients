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

namespace Mishimation
{
    public class DynamicAnimation : AnimationBase
    {
        Func<double, float, float, double, float> easing;
        private AnimatableProperty initValue, endValue;
        AnimatableProperty property;

        public DynamicAnimation(AnimatableProperty init, AnimatableProperty end, AnimatableProperty prop, double duration, Func<double, float, float, double, float> easing)
            : base(duration)
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
            double currentValue = (double)easing(timestep, Convert.ToSingle(initValue.Get()), Convert.ToSingle(endValue.Get()), duration);
            this.property.Set(currentValue);
        }
    }
}

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
    public abstract class Animatable
    {
        public object GetValue(AnimatableProperty prop)
        {
            return prop.Get();
        }

        public void SetValue(AnimatableProperty property, Object value)
        {
            property.Set(value);
        }
    }
}

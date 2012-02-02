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
    public class AnimatableProperty
    {
        string property;
        Type dataType;
        object data;

        public AnimatableProperty(string property, Type dataType, object defaultValue)
        {
            this.property = property;
            this.dataType = dataType;
            this.data = defaultValue;
        }

        public static AnimatableProperty Register(string propertyName, Type dataType, object defaultValue)
        {
            return new AnimatableProperty(propertyName, dataType, defaultValue);
        }

        public void Set(object value)
        {
            data = value;
        }

        public object Get()
        {
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Mishimation
{
    public static class Easings
    {
        // t: current time
        // b: start value
        // c: change in value
        // d: duration
        // (t and d can be frames or seconds/milliseconds
        static Random rand = new Random();

        public static Func<float, float, float> RandomFloatBetween = (float min, float max) =>
        {
            return min + (float)rand.NextDouble() * (max - min);
        };

        public static Func<int, int, int> RandomIntBetween = (int min, int max) =>
        {
            return min + rand.Next() * (max - min);
        };

        public static Func<double, double, double> RandomDoubleBetween = (double min, double max) =>
        {
            return max - rand.NextDouble() * (max - min);
        };

        public static Func<double, float, float, double, float> easeInBounce = (double time, float initVal, float endVal, double duration) =>
        {
	        return (float)endVal - Easings.easeOutBounce (duration-time, 0, endVal, duration) + initVal;
        };

        // bounce easing out
        public static Func<double, float, float, double, float> easeOutBounce = (double time, float initVal, float endVal, double duration) =>
        {
            if ((time /= duration) < (1 / 2.75))
            {
                return (float)(endVal * (7.5625 * time * time) + initVal);
            }
            else if (time < (2 / 2.75))
            {
                return (float)(endVal * (7.5625 * (time -= (1.5f / 2.75f)) * time + .75) + initVal);
            }
            else if (time < (2.5 / 2.75))
            {
                return (float)(endVal * (7.5625 * (time -= (2.25f / 2.75f)) * time + .9375) + initVal);
            }
            else
            {
                return (float)(endVal * (7.5625 * (time -= (2.625f / 2.75f)) * time + .984375) + initVal);
            }
        };

        // bounce easing in/out
        public static Func<double, float, float, double, float> easeInOutBounce = (double time, float initVal, float endVal, double duration) =>
        {
            if (time < duration / 2) return (float)(easeInBounce(time * 2, 0, endVal, duration) * .5 + initVal);
            return (float)(easeOutBounce(time * 2 - duration, 0, endVal, duration) * .5 + endVal * .5 + initVal);
        };

        public static Func<double, float, float, double, float> Linear = (double time, float initVal, float endVal, double duration) =>
        {
            return endVal * (float)time / (float)duration + initVal;
        };

        public static Func<double, float, float, double, float> QuadraticIn = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            return endVal * (float)time * (float)time + initVal; 
        };

        public static Func<double, float, float, double, float> QuadraticOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            return -endVal * (float)time * ((float)time - 2) + initVal;
        };

        public static Func<double, float, float, double, float> QuadraticInOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration / 2;
            if (time < 1) return endVal / 2 * (float)time * (float)time + initVal;
            time--;
            return -endVal / 2 * ((float)time * ((float)time - 2) - 1) + initVal;
        };

        public static Func<double, float, float, double, float> CubicOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            time--;
            return endVal * ((float)time * (float)time * (float)time + 1) + initVal;
        };

        public static Func<double, float, float, double, float> CubicIn = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            return endVal * (float)time * (float)time * (float)time + initVal;
        };

        public static Func<double, float, float, double, float> CubicInOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration / 2;
            if (time < 1) return endVal / 2 * (float)time * (float)time * (float)time + initVal;
            time -= 2;
            return endVal / 2 * ((float)time * (float)time * (float)time + 2) + initVal;
        };

        public static Func<double, float, float, double, float> QuarticIn = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            return endVal * (float)time * (float)time * (float)time * (float)time + initVal;
        };

        public static Func<double, float, float, double, float> QuarticOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            time--;
            return -endVal * ((float)time * (float)time * (float)time * (float)time - 1) + initVal;
        };

        public static Func<double, float, float, double, float> QuarticInOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration / 2;
            if (time < 1) return endVal / 2 * (float)time * (float)time * (float)time * (float)time + initVal;
            time -= 2;
            return -endVal / 2 * ((float)time * (float)time * (float)time * (float)time - 2) + initVal;
        };

        public static Func<double, float, float, double, float> QuinticIn = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            return endVal * (float)time * (float)time * (float)time * (float)time * (float)time + initVal;
        };

        public static Func<double, float, float, double, float> QuinticOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            time--;
            return endVal * ((float)time * (float)time * (float)time * (float)time * (float)time + 1) + initVal;
        };

        public static Func<double, float, float, double, float> QuinticInOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration / 2;
            if (time < 1) return endVal / 2 * (float)time * (float)time * (float)time * (float)time * (float)time + initVal;
            time -= 2;
            return endVal / 2 * ((float)time * (float)time * (float)time * (float)time * (float)time + 2) + initVal;
        };

        public static Func<double, float, float, double, float> SineIn = (double time, float initVal, float endVal, double duration) =>
        {
            return -endVal * (float)Math.Cos(time / duration * (Math.PI / 2)) + endVal + initVal;
        };

        public static Func<double, float, float, double, float> SineOut = (double time, float initVal, float endVal, double duration) =>
        {
            return endVal * (float)Math.Sin(time / duration * (Math.PI / 2)) + initVal;
        };

        public static Func<double, float, float, double, float> SineInOut = (double time, float initVal, float endVal, double duration) =>
        {
            return -endVal / 2 * (float)(Math.Cos(Math.PI * time / duration) - 1) + initVal;
        };

        public static Func<double, float, float, double, float> ExponentialIn = (double time, float initVal, float endVal, double duration) =>
        {
            return endVal * (float)Math.Pow(2, 10 * (time / duration - 1)) + initVal;
        };

        public static Func<double, float, float, double, float> ExponentialOut = (double time, float initVal, float endVal, double duration) =>
        {
            return endVal * (-(float)Math.Pow(2, -10 * time / duration) + 1) + initVal;
        };

        public static Func<double, float, float, double, float> ExponentialInOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration / 2;
            if (time < 1) return endVal / 2 * (float)Math.Pow(2, 10 * (time - 1)) + initVal;
            time--;
            return endVal / 2 * (float)(-Math.Pow(2, -10 * time) + 2) + initVal;
        };

        public static Func<double, float, float, double, float> CircularIn = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            return -endVal * (float)(Math.Sqrt(1 - time * time) - 1) + initVal;
        };

        public static Func<double, float, float, double, float> CircularOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration;
            time--;
            return endVal * (float)Math.Sqrt(1 - time * time) + initVal;
        };

        public static Func<double, float, float, double, float> CircularInOut = (double time, float initVal, float endVal, double duration) =>
        {
            time /= duration / 2;
            if (time < 1) return -endVal / 2 * (float)(Math.Sqrt(1 - time * time) - 1) + initVal;
            time -= 2;
            return endVal / 2 * (float)(Math.Sqrt(1 - time * time) + 1) + initVal;
        };

        public static Func<double, float, float, double, float> Sine = (double time, float initVal, float endVal, double duration) =>
        {
            return (float)( ((endVal - initVal) * Math.Sin((time / duration) * 2 * Math.PI)) + (endVal - initVal) + initVal );
        };
    }
}

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

namespace WordCruncherWP7
{
    public class Helpers
    {
        public static Random Random = new Random();

        public static float RandomBetween(float min, float max)
        {
            return min + (float)Random.NextDouble() * (max - min);
        }

        public static int RandomBetween(int min, int max)
        {
            return min + Random.Next() * (max - min);
        }

        public static double RandomBetween(double min, double max)
        {
            return max - Random.NextDouble() * (max - min);
        }
    }
}

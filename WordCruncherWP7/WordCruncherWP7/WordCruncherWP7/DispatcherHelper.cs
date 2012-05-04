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
using System.Windows.Threading;

namespace WordCruncherWP7
{
    public static class DispatcherHelper
    {
        public static void DelayInvoke(this Dispatcher dispatcher, TimeSpan ts, Action action)
        {
            DispatcherTimer delayTimer = new DispatcherTimer();
            delayTimer.Interval = ts;
            delayTimer.Tick += (s, e) =>
            {
                delayTimer.Stop();
                action();
            };
            delayTimer.Start();
        }
    }
}

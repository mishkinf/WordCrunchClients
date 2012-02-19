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
    using System;

    public class StopWatch
    {

        private DateTime endTime;
        private DateTime stopTime;
        private bool running = false;

        public void Start()
        {
            this.endTime = DateTime.Now.AddMinutes(2.0);
            this.running = true;
        }

        public void Stop()
        {
            stopTime = DateTime.Now;
            this.running = false;
        }

        // elaspsed time in milliseconds
        public double GetElapsedTime()
        {
            TimeSpan interval = GetTimeSpanInterval();

            return interval.TotalMilliseconds;
        }


        public TimeSpan GetTimeSpanInterval()
        {
            TimeSpan interval;

            if (running)
            {
                if (endTime < DateTime.Now)
                    endTime = DateTime.Now;

                interval = endTime - DateTime.Now;
            }
            else
            {
                interval = stopTime - endTime;
            }

            return interval;
        }

        public String GetFormattedTimeRemaining()
        {
            TimeSpan interval = GetTimeSpanInterval();

            return interval.ToString("m\\:ss");
        }


        // elaspsed time in seconds
        public double GetElapsedTimeSecs()
        {
            TimeSpan interval = GetTimeSpanInterval();

            return interval.TotalSeconds;
        }
    }
        
}

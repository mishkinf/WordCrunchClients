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
    public abstract class AnimationBase
    {
        double timestep = 0, duration;
        AnimationState state = AnimationState.Stopped;

        public AnimationBase(double duration)
        {
            this.duration = duration;
        }

        public bool IsPlaying
        {
            get { return state == AnimationState.Playing; }
        }

        public void PerformUpdate()
        {
            if (timestep < this.duration)
            {
                Update(timestep++, duration);
            }
        }

        public abstract void Update(double timestep, double duration);

        public void Play()
        {
            Reset();
            state = AnimationState.Playing;
        }

        protected void ResetTime()
        {
            timestep = 0;
        }

        public abstract void Reset();

        public void Stop()
        {
            state = AnimationState.Stopped;
        }
    }
}

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
using System.Collections.Generic;

namespace Mishimation
{
    public static class Animator
    {
        private static List<AnimationBase> Animations = new List<AnimationBase>();

        public static void AddAnimation(AnimationBase a)
        {
            Animator.Animations.Add(a);
        }

        public static void Update()
        {
            foreach(AnimationBase a in Animator.Animations) {
                if(a.IsPlaying)
                    a.PerformUpdate();
            }
        }
    }
}

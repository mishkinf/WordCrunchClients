using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Mishimation;

namespace WordCruncherWP7
{
    public class GameWall : Animatable
    {
        public Rectangle collisionRect;
        public int x, y, index1, index2;
        public bool enabled = false;

        public GameWall(Rectangle rect, int index1, int index2)
        {
            this.collisionRect = rect;
            this.index1 = index1;
            this.index2 = index2;
        }
    }
}

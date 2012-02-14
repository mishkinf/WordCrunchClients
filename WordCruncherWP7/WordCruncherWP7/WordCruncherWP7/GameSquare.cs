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
    public class GameSquare : Animatable
    {
        public readonly AnimatableProperty ScaleProperty = AnimatableProperty.Register("Scale", typeof(float), 1.0f);

        public float Scale
        {
            get
            {
                return (float)GetValue(ScaleProperty);
            }
            set
            {
                SetValue(ScaleProperty, value);
            }
        }

        public Dictionary<string, int> letterValue = new Dictionary<string, int>();
        public string letter;
        public Rectangle rect, collisionRect;
        public Color color;
        public int x, y;
        private float scale = 1.0f;
        public int value = 1;
        public int index = 0;
        public bool bombed = false;
        public bool hasBomb = false;
        public bool yourBomb = false;

        public GameSquare(string letter, int value, Rectangle rect, Color c, int x, int y, int index)
        {
            this.x = x;
            this.y = y;
            this.letter = letter;
            this.rect = rect;
            int padding = 9;
            this.collisionRect = new Rectangle(rect.X + padding, rect.Y + padding, rect.Width - 2*padding, rect.Height - 2*padding);
            this.value = value;
            this.color = c;
            this.index = index;
        }

        public int GetValue()
        {
            return this.value;
        }

        public int GetServerIndex()
        {
            return index + 1;
        }

    }
}

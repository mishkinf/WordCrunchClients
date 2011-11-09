using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using Microsoft.Xna.Framework;

namespace WordCruncherWP7
{
    public class GameSquare
    {
        public Dictionary<string, int> letterValue = new Dictionary<string, int>();
        public string letter;
        public Rectangle rect, collisionRect;
        public Color color;
        public int x, y;

        public GameSquare(string letter, Rectangle rect, Color c, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.letter = letter;
            this.rect = rect;
            int padding = 10;
            this.collisionRect = new Rectangle(rect.X + padding, rect.Y + padding, rect.Width - 2*padding, rect.Height - 2*padding);

            this.color = c;

            letterValue.Add("a", 1);
            letterValue.Add("b", 2);
            letterValue.Add("c", 3);
            letterValue.Add("d", 4);
            letterValue.Add("e", 5);
            letterValue.Add("f", 6);
            letterValue.Add("g", 7);
            letterValue.Add("h", 8);
            letterValue.Add("i", 9);
            letterValue.Add("j", 10);
            letterValue.Add("k", 11);
            letterValue.Add("l", 12);
            letterValue.Add("m", 13);
            letterValue.Add("n", 14);
            letterValue.Add("o", 15);
            letterValue.Add("p", 16);
            letterValue.Add("q", 17);
            letterValue.Add("r", 18);
            letterValue.Add("s", 19);
            letterValue.Add("t", 20);
            letterValue.Add("u", 21);
            letterValue.Add("v", 22);
            letterValue.Add("w", 23);
            letterValue.Add("x", 24);
            letterValue.Add("y", 25);
            letterValue.Add("z", 26);
        }

        public int GetValue()
        {
            return letterValue[letter];
        }

    }
}

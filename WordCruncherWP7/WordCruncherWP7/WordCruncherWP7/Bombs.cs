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
using Microsoft.Xna.Framework;
using WordCruncherWP7.Messages;
using System.Collections.Generic;

namespace WordCruncherWP7
{
    public class Bombs
    {
        int ticks = 0;
        public bool bombing = false;
        static List<int> bombedIndices = new List<int>();

        public Bombs() 
        {
        }

        public void DropBomb(int[] indices)
        {
            ResetBombs();
            bombedIndices.AddRange(indices);
        }

        public void DropBomb(int index) 
        {
            ResetBombs();
            bombedIndices.Add(index);
        }

        private void Reset()
        {
            ticks = 100;
            bombing = true;
        }

        public void UpdateBomb()
        {
            if (ticks <= 0)
                bombing = false;
            else if (bombing)
                ticks--;
        }

        public float GetScaleOffset()
        {
            double sine = (Math.Sin(ticks / 3.0) + 1) / 3.0;
            float scaleOffset = (float)((ticks / 100.0) * sine);

            return scaleOffset;
        }

        public bool IsBombing(int index)
        {
            return bombing && bombedIndices != null && bombedIndices.Contains(index);
        }

        public void AddBomb(Vector2 tap)
        {
            foreach (GameSquare gs in WordGame.squares)
            {
                if (gs.collisionRect.Contains((int)tap.X, (int)tap.Y))
                {
                    gs.hasBomb = true;
                    CrunchCore.SendMessage(new DropBombMessage(gs.index ));
                }
            }
        }

        public void ResetBombs()
        {
            foreach (int index in bombedIndices)
            {
                foreach (GameSquare s in WordGame.squares)
                {
                    if (s.index == index)
                        s.hasBomb = false;
                }
            }

            bombedIndices.Clear();
        }
    }

   
}

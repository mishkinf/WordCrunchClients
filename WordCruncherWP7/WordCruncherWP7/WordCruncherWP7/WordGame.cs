using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Linq.Expressions;

namespace WordCruncherWP7
{
    

    public class WordGame
    {
        public int columns;
        public int rows;
        private int boxSize;
        private int boxPadding;
        string[] letters = new string[26] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        static Random rand = new Random();
        public List<GameSquare> squares = new List<GameSquare>();
        public List<GameSquare> selectedSquares = new List<GameSquare>();
        public bool Selecting = false;

        public bool IsValidNextSquare(GameSquare current, GameSquare next)
        {
            bool value = false;

            try
            {
                value = Math.Abs(next.x - current.x) <= 1 && Math.Abs(next.y - current.y) <= 1;
            }
            catch
            {
            }

            return value;
        }

        public string GetLetters()
        {
            return String.Join(", ", selectedSquares.Select(s => s.letter));
        }

        public WordGame(int width, int height) {
            this.columns = 4;
            this.rows = 6;

            SetSquareSizes(width, height);

            for (int r = 0; r < this.rows; r++)
            {
                for (int c = 0; c < this.columns; c++)
                {

                    squares.Add(new GameSquare(letters[rand.Next(26)], new Rectangle(c * (boxSize + boxPadding), r * (boxSize + boxPadding), boxSize, boxSize), Color.Blue, c, r));
                }
            }
	    }

        private void SetSquareSizes(int width, int height)
        {
            int side = Math.Min(width, height);
            int boxSide;

            if (width < height)
            {
                boxSide = side/this.columns;
            }
            else
            {
                boxSide = side/this.rows;
            }

            boxSize = (int) (boxSide*0.8f);
            boxPadding = (int) ((boxSide*0.2f)/2);
        }
    }

}

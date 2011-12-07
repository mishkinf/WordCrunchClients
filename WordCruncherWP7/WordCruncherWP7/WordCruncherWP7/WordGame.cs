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
    public static class WordGame
    {
        public static int columns = 5;
        public static int rows = 5;
        private static int boxSize;
        private static int boxPadding;
        static string[] letters = new string[26] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        static Random rand = new Random();
        public static GameSquare[,] squares = new GameSquare[5,5];
        public static List<GameSquare> selectedSquares = new List<GameSquare>();
        public static bool Selecting = false;
        public static int player1Score = 0;
        public static int player2Score = 0;

        public static bool IsValidNextSquare(GameSquare current, GameSquare next)
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

        public static string GetLetters()
        {
            return String.Join(", ", selectedSquares.Select(s => s.letter));
        }

        public static void SetWordGameSize(int cols, int rows) {
            WordGame.columns = cols;
            WordGame.rows = rows;
	    }

        public static void InitGame(int width, int height)
        {
            //WordGame.InitBoard();
            WordGame.SetSquareSizes(width, height);
        }

        private static void InitBoard()
        {
            WordGame.squares = new GameSquare[WordGame.columns, WordGame.rows];
        }

        public static void SetSquare(string letter, int value, int c, int r, int index)
        {
            WordGame.squares[c,r] = new GameSquare(letter, value, new Rectangle(c * (boxSize + boxPadding), r * (boxSize + boxPadding), boxSize, boxSize), Color.Blue, c, r, index);
        }

        private static void SetSquareSizes(int width, int height)
        {
            int side = Math.Min(width, height);
            int boxSide;

            if (width < height)
            {
                boxSide = side/WordGame.columns;
            }
            else
            {
                boxSide = side/WordGame.rows;
            }

            boxSize = (int) (boxSide*0.8f);
            boxPadding = (int) ((boxSide*0.2f)/2);
        }
    }

}

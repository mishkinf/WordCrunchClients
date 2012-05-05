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
    public class Word
    {
        public int ID;
        public int[] indices;
        public GameSquare[] GameSquares;
        public bool Bombed = false;
        public int WordValue
        {
            get
            {
                int val = 0;

                foreach (GameSquare s in GameSquares)
                    val += s.value;

                return val;
            }
        }

        public String WordString {
            get 
            {
                String val = "";

                foreach(GameSquare s in GameSquares)
                    val += s.letter;

                return val;
            }
        }

        public Word(int ID, int[] indices, GameSquare[] squares)
        {
            this.ID = ID;
            this.indices = indices;
            this.GameSquares = squares;
        }

        public void SetBombed() 
        {
            //foreach (GameSquare s in GameSquares)
            //{
            //    s.hasBomb = true;
            //}
        }

        public override string ToString()
        {
            return WordValue + " - " + WordString;
        }
    }
}

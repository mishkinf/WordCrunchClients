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
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WordCruncherWP7.Messages
{
    public class StartGameMessage : iMessage, iDecodableMessage
    {
        public String player1, player2;
        public int bombCount, columns, rows;                                                                                    
        public static StartGameMessage fromJSON(string message)
        {
            JObject o = JObject.Parse(message);

            string player1 = (string)o["players"][0];
            string player2 = (string)o["players"][1];

            int bomb_count = (int)o["bomb_count"];

            int columns = (int)o["board"]["columns"];
            int rows = (int)o["board"]["rows"];

            int total = columns*rows;
            List<GameSquare> squares = new List<GameSquare>();

            WordGame.SetWordGameSize(columns, rows);

            int index = 0;
            for(int c = 0; c < columns; c++)
            {
                for(int r = 0; r < rows; r++) {
                    int letter_ascii = (int)o["board"]["matrix"][index][0];
                    string letter = System.Convert.ToChar(letter_ascii).ToString();
                    int value = (int)o["board"]["matrix"][index][1];

                    WordGame.SetSquare(letter, value, r, c, index);

                    if (index < 24)
                    {
                        //int x = WordGame.squares[r, c].x + WordGame.squares[r, c].rect.Width - 15;
                        //int y = WordGame.squares[r, c].y;
                        //int width = 30;
                       // int height = WordGame.squares[r, c].rect.Height;

                        //WordGame.walls.Add(new GameWall(new Microsoft.Xna.Framework.Rectangle(x, y, width, height), index, index + 1));
                    }
                    
                    index++;
                }
            }

            

            return new StartGameMessage(player1, player2, bomb_count, columns, rows);
        }

        public StartGameMessage(String player1, String player2, int bombCount, int columns, int rows)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.bombCount = bombCount;
            this.columns = columns;
            this.rows = rows;
        }
    }
}

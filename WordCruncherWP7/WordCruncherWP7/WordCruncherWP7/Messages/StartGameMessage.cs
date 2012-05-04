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
        public String player_id, user1, user2;
        public int bombCount, columns, rows;                                                                                    
        public static StartGameMessage fromJSON(string message)
        {
            JObject o = JObject.Parse(message);


            IEnumerable<JToken> keys = o.Children();
            string player_id = o["player_id"].ToString();
            string player1 = (string)o["users"][0];
            string player2 = (string)o["users"][1];

            int bomb_count = (int)o["bomb_count"];

            int columns = Int32.Parse(o["board"]["cols"].ToString());
            int rows = Int32.Parse(o["board"]["rows"].ToString());

            int total = columns*rows;
            List<GameSquare> squares = new List<GameSquare>();

            WordGame.SetWordGameSize(columns, rows);

            int index = 0;
            for(int c = 0; c < columns; c++)
            {
                for(int r = 0; r < rows; r++) {
                    int letter_ascii = (int)o["board"]["matrix"][index]["letter"];
                    string letter = System.Convert.ToChar(letter_ascii).ToString();
                    int value = (int)o["board"]["matrix"][index]["value"];

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

            

            return new StartGameMessage(player_id, bomb_count, columns, rows, player1, player2);
        }

        public StartGameMessage(String player_id, int bombCount, int columns, int rows, string player1, string player2)
        {
            this.user1 = player1;
            this.user2 = player2;
            this.player_id = player_id;
            this.bombCount = bombCount;
            this.columns = columns;
            this.rows = rows;
        }
    }
}

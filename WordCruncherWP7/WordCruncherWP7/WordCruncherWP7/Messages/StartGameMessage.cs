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
    public class StartGameMessage : iMessage
    {                                                                                                   
        public void fromJSON(string message)
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
                
                    index++;
                }
            }
        }

        public String encode() { return "";  }
    }
}

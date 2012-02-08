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

namespace WordCruncherWP7.Messages
{
    public class BadGuessResponseMessage : iMessage, iDecodableMessage
    {
        public int player_index, id, player1Score, player2Score;
        public int[] selection, bombs;

        public void fromJSON(string json)
        {
            JObject o = JObject.Parse(json);
            this.id = (int)o["id"];
            this.player1Score = (int)o["scores"][0];
            this.player2Score = (int)o["scores"][1];
        }

        public BadGuessResponseMessage(int id, int[] scores)
        {
            this.id = id;
        }
    }
}

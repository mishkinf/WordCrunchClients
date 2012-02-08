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
    public class BombedGuessResponseMessage : iMessage, iDecodableMessage
    {
        public int id;
        public int scoreYou, scoreOpponent;
        public int[] bombs;
        public int player_index;

        public static BombedGuessResponseMessage fromJSON(string message) {
            JObject o = JObject.Parse(message);
            int id = (int) o["id"];
            int scoreYou = -1, scoreOpponent = -1, length;
            int[] bombs;
            int player_index = (int)o["player_index"];
           
            if (Constants.PlayerIndex == 1)
            {
                scoreYou = (int)o["scores"][0];
                scoreOpponent = (int)o["scores"][1];
            }
            else if (Constants.PlayerIndex == 2)
            {
                scoreYou = (int)o["scores"][1];
                scoreOpponent = (int)o["scores"][0];
            }

            JArray bombsJson = (JArray) o["bombs"];
            length = bombsJson.Count;
            bombs = new int[length];
            for(int i = 0; i < length; i++) {
                bombs[i] = (int) bombsJson[i] - 1;
            }

            return new BombedGuessResponseMessage(id, scoreYou, scoreOpponent, bombs, player_index);
        }

        public BombedGuessResponseMessage(int id, int scoreYou, int scoreOpponent, int[] bombs, int player_index) {
            this.id = id;
            this.scoreYou = scoreYou;
            this.scoreOpponent = scoreOpponent;
            this.bombs = bombs;
            this.player_index = player_index;
        }
    }
}

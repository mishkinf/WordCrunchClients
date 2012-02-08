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
    public class GoodGuessResponseMessage : iMessage, iDecodableMessage
    {
        public int id;
        public int scoreYou, scoreOpponent;

        public static GoodGuessResponseMessage fromJSON(string json) {
            JObject o = JObject.Parse(json);
            int id = (int)o["id"];
            int scoreYou = -1, scoreOpponent = -1;

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

            return new GoodGuessResponseMessage(id, scoreYou, scoreOpponent);
        }

        public GoodGuessResponseMessage(int id, int scoreYou, int scoreOpponent) {
            this.id = id;
            this.scoreYou = scoreYou;
            this.scoreOpponent = scoreOpponent;
        }
    }
}

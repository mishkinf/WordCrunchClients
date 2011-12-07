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
    public class GoodGuessResponse : iMessage
    {
        public static int TYPE_CODE = 0x0011;

        public int id;
        public int[] scores;

        public void fromJSON(string json) {
            JObject o = JObject.Parse(json);
            this.id = (int)o["id"];
            int player1Score = (int)o["scores"][0];
            int player2Score = (int)o["scores"][1];

            this.scores = new int[2] { player1Score, player2Score };

            //return new GoodGuessResponse(id, scores);
        }

        public GoodGuessResponse()
        {
        }

        public GoodGuessResponse(int id, int[] scores) {
            this.id = id;
            this.scores = scores;
        }

        public String encode() { return ""; }
    }
}

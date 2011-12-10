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
    public class BombedGuessResponse : iMessage
    {
        public static int TYPE_CODE = 0x0012;

        public int id;
        public int[] scores;
        public int[] bombs;

        public void fromJSON(string message) {
            JObject o = JObject.Parse(message);
            int id = (int) o["id"];
            int[] scores;
            int[] bombs;

            JArray scoresJson = (JArray) o["scores"];
            int length = scoresJson.Count;
            scores = new int[length];

            for(int i = 0; i < length; i++) {
                scores[i] = (int)scoresJson[i];
            }

            JArray bombsJson = (JArray) o["bombs"];
            length = bombsJson.Count;
            bombs = new int[length];
            for(int i = 0; i < length; i++) {
                bombs[i] = (int) bombsJson[i];
            }


        }

        public BombedGuessResponse(int id, int[] scores, int[] bombs) {
            this.id = id;
            this.scores = scores;
            this.bombs = bombs;
        }

        public BombedGuessResponse()
        {
         
        }

        public String encode() { return ""; }
    }
}

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

namespace WordCruncherWP7.Messages
{
    public class GoodGuessResponse : Message
    {
        /*
        public static int TYPE_CODE = 0x0011;

        public int id;
        public int[] scores;

        public static GoodGuessResponse fromJson(JSONObject json) {
            int id = ((Long) json.get("id")).intValue();
            int[] scores;

            JSONArray scoresJson = (JSONArray) json.get("scores");
            int length = scoresJson.size();
            scores = new int[length];
            for(int i = 0; i < length; i++) {
                scores[i] = ((Long) scoresJson.get(i)).intValue();
            }

            return new GoodGuessResponse(id, scores);
        }

        public GoodGuessResponse(int id, int[] scores) {
            this.id = id;
            this.scores = scores;
        }*/

        public String encode() { return ""; }
    }
}

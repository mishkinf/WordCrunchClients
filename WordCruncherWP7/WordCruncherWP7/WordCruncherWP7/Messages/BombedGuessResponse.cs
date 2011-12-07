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
    public class BombedGuessResponse : iMessage
    {
        /*
        public static int TYPE_CODE = 0x0012;

        public int id;
        public int[] scores;
        public int[] bombs;

        public static BombedGuessResponse fromJson(JSONObject json) {
            int id = ((Long) json.get("id")).intValue();
            int[] scores;
            int[] bombs;

            JSONArray scoresJson = (JSONArray) json.get("scores");
            int length = scoresJson.size();
            scores = new int[length];
            for(int i = 0; i < length; i++) {
                scores[i] = ((Long) scoresJson.get(i)).intValue();
            }

            JSONArray bombsJson = (JSONArray) json.get("bombs");
            length = bombsJson.size();
            bombs = new int[length];
            for(int i = 0; i < length; i++) {
                bombs[i] = ((Long) bombsJson.get(i)).intValue();
            }

            return new BombedGuessResponse(id, scores, bombs);
        }

        public BombedGuessResponse(int id, int[] scores, int[] bombs) {
            this.id = id;
            this.scores = scores;
            this.bombs = bombs;
        }*/

        public void fromJSON(string message)
        {

        }

        public String encode() { return ""; }
    }
}

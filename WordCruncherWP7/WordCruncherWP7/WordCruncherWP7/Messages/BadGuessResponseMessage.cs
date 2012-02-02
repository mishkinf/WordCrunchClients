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
    public class BadGuessResponseMessage : iMessage
    {
        public string type;
        public int player_index;
        public int id;
        public int[] selection;
        public int[] bombs;
        public int player1Score, player2Score;

  //       type: "bombed_guess_response",
  //       player_index: 2 // the index of the player who guessed the bombed word
  //id: 321312321, // id of the guess_word message
  //selection: [1,2,3,4] // selection path
  //bombs: [2,4], // indexes of where the bombs were
  //scores: [23, 17] // updated game scores

        public void fromJSON(string json) {
            JObject o = JObject.Parse(json);
            this.id = (int)o["id"];
            this.player1Score = (int)o["scores"][0];
            this.player2Score = (int)o["scores"][1];

            //this.scores = new int[2] { player1Score, player2Score };

            //return new GoodGuessResponse(id, scores);
        }

        public BadGuessResponseMessage()
        {
        }

        public BadGuessResponseMessage(int id, int[] scores)
        {
            this.id = id;
            //this.scores = scores;
        }

        public String encode() { return ""; }
    }
}

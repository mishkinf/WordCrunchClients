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
        public int scoreYou, scoreOpponent;
        public Word Word;
        public ePlayer Player;

        public static GoodGuessResponseMessage fromJSON(string json) {
            JObject o = JObject.Parse(json);
            int length, id = (int)o["id"];
            int[] selectionPath;
            GameSquare[] squares;
            int scoreYou = -1, scoreOpponent = -1;
            Word word;
            ePlayer player;

            if ((int)o["player_index"] == Globals.PlayerIndex)
                player = ePlayer.You;
            else
                player = ePlayer.Opponent;

            if (Globals.PlayerIndex == 1)
            {
                scoreYou = (int)o["scores"][0];
                scoreOpponent = (int)o["scores"][1];
            }
            else if (Globals.PlayerIndex == 2)
            {
                scoreYou = (int)o["scores"][1];
                scoreOpponent = (int)o["scores"][0];
            }

            JArray selectionJson = (JArray)o["selection"];
            length = selectionJson.Count;
            selectionPath = new int[length];
            for (int i = 0; i < length; i++)
            {
                selectionPath[i] = (int)selectionJson[i] - 1;
            }

            squares = new GameSquare[selectionPath.Length];

            for (int i = 0; i < squares.Length; i++)
                squares[i] = WordGame.squares2[selectionPath[i]];

            word = new Word(id, selectionPath, squares);
            return new GoodGuessResponseMessage(scoreYou, scoreOpponent, player, word);
        }

        public GoodGuessResponseMessage(int scoreYou, int scoreOpponent, ePlayer player, Word word) {
            this.scoreYou = scoreYou;
            this.scoreOpponent = scoreOpponent;
            this.Player = player;
            this.Word = word;
        }
    }
}

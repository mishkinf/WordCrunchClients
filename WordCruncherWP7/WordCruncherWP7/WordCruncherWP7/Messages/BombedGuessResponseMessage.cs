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
        public int[] selectionPath;
        public int player_index;
        public ePlayer Player;
        public bool YourGuess { get { return Player == ePlayer.You; } }
        public Word Word;

        public static BombedGuessResponseMessage fromJSON(string message) {
            JObject o = JObject.Parse(message);
            int id = (int) o["id"];
            int scoreYou = -1, scoreOpponent = -1, length;
            int[] bombs, selectionPath;
            int player_index = (int)o["player_id"];
            ePlayer player;
            Word word;
            GameSquare[] squares;

            if (Globals.PlayerIndex == player_index)
                player = ePlayer.You;
            else
                player = ePlayer.Opponent;

            if (Globals.PlayerIndex == 0)
            {
                scoreYou = (int)o["scores"][0];
                scoreOpponent = (int)o["scores"][1];
            }
            else if (Globals.PlayerIndex == 1)
            {
                scoreYou = (int)o["scores"][1];
                scoreOpponent = (int)o["scores"][0];
            }

            JArray bombsJson = (JArray) o["bombs"];
            length = bombsJson.Count;
            bombs = new int[length];
            for(int i = 0; i < length; i++) {
                bombs[i] = (int) bombsJson[i] ;
            }

            JArray selectionJson = (JArray)o["selection"];
            length = selectionJson.Count;
            selectionPath = new int[length];
            for (int i = 0; i < length; i++)
            {
                selectionPath[i] = (int)selectionJson[i];
            }

            squares = new GameSquare[selectionPath.Length];

            for (int i = 0; i < squares.Length; i++)
                squares[i] = WordGame.squares2[selectionPath[i]];

            word = new Word(id, selectionPath, squares);
            return new BombedGuessResponseMessage(scoreYou, scoreOpponent, word, bombs, player);
        }

        public BombedGuessResponseMessage(int scoreYou, int scoreOpponent, Word word, int[] bombs, ePlayer player) {
            this.scoreYou = scoreYou;
            this.scoreOpponent = scoreOpponent;
            this.bombs = bombs;
            this.Word = word;
            this.Player = player;
        }
    }
}

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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WordCruncherWP7.Messages
{
    public class HighScoresResponseMessage : iMessage, iDecodableMessage 
    {
        //public ObservableCollection<MatchResult> matchResults = new ObservableCollection<MatchResult>();

        public static HighScoresResponseMessage fromJSON(string json)
        {
            JObject o = JObject.Parse(json);

           // ObservableCollection<MatchResult> results = new ObservableCollection<MatchResult>();
            JArray games = (JArray)o["games"];
            JArray game;
            String opponentUsername;
            int yourScore, opponentScore;

            for (int i = 0; i < games.Count; i++ )
            {
                game = (JArray)games[i];
                opponentUsername = (String)game[1]["username"];
                opponentScore = (int)game[1]["score"];
                yourScore = (int)game[0]["score"];
                Globals.MatchResults.Add(new MatchResult(yourScore, opponentScore, opponentUsername));
            }
                

            return new HighScoresResponseMessage();
        }

        public HighScoresResponseMessage()
        {
        }
    }
}

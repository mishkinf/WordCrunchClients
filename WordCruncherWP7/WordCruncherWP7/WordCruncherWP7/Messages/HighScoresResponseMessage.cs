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
        public ObservableCollection<MatchResult> matchResults = new ObservableCollection<MatchResult>();

        public static HighScoresResponseMessage fromJSON(string json)
        {
            JObject o = JObject.Parse(json);

            ObservableCollection<MatchResult> results = new ObservableCollection<MatchResult>();
            JArray scores = (JArray)o["scores"];
            JArray opponents = (JArray)o["players"];

            for (int i = 0; i < scores.Count; i++ )
            {
                results.Add(new MatchResult((int)scores[i][0], (int)scores[i][1], (String)opponents[i]));
            }
                

            return new HighScoresResponseMessage(results);
        }

        public HighScoresResponseMessage(ObservableCollection<MatchResult> results)
        {
            this.matchResults = results;
        }
    }
}

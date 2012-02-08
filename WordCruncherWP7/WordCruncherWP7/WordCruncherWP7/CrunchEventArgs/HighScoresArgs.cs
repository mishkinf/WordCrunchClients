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
using WordCruncherWP7.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WordCruncherWP7.CrunchEventArgs
{
    public class HighScoresArgs: EventArgs
    {
        public ObservableCollection<MatchResult> matchResults;

        public HighScoresArgs(ObservableCollection<MatchResult> matchResults)
        {
            this.matchResults = matchResults;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using WordCruncherWP7.Messages;

namespace WordCruncherWP7
{
    public partial class HighScores : PhoneApplicationPage
    {
        private ObservableCollection<MatchResult> matchResults = new ObservableCollection<MatchResult>();
        public ObservableCollection<MatchResult> MatchResults { get { return matchResults; } }

        public HighScores()
        {
            InitializeComponent();

            CrunchCore.OnHighScoresReturned += new EventHandler<CrunchEventArgs.HighScoresArgs>(CrunchCore_OnHighScoresReturned);

            CrunchCore.SendMessage(new HighScoresMessage());
        }

        void CrunchCore_OnHighScoresReturned(object sender, CrunchEventArgs.HighScoresArgs e)
        {
            this.matchResults = e.matchResults;
        }


    }
}

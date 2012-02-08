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

namespace WordCruncherWP7
{
    public class MatchResult
    {
        private int yourScore = 0;
        private int opponentsScore = 0;
        private String opponentsUsername = "fakeUser";

        public int YourScore { get { return yourScore; } }
        public int OpponentsScore { get { return opponentsScore; } }
        public String OpponentsUsername { get { return opponentsUsername; } }
        public bool Result { get { return YourScore > OpponentsScore; } }

        public MatchResult(int yourScore, int opponentsScore, String opponentsUsername)
        {
            this.yourScore = yourScore;
            this.opponentsScore = opponentsScore;
            this.opponentsUsername = opponentsUsername;
        }
    }
}

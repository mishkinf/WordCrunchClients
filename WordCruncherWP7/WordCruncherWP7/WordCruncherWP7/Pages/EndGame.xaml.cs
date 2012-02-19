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

namespace WordCruncherWP7
{
    public partial class EndGame : PhoneApplicationPage
    {
        public EndGame()
        {
            CrunchCore.Disconnect();
            InitializeComponent();

            if (WordGame.scoreYou > WordGame.scoreOpponent)
                gameStatus.Text = "You Won.";
            else if (WordGame.scoreOpponent > WordGame.scoreYou)
                gameStatus.Text = "You Lost.";
            else
                gameStatus.Text = "Tie Game.";

            opponentNameBox.Text = Globals.OpponentUsername;
            yourNameBox.Text = Globals.YourUsername;
            yourWords.ItemsSource = CrunchCore.yourWords;
            opponentsWords.ItemsSource = CrunchCore.opponentsWords;

            yourScore.Text = WordGame.scoreYou.ToString();
            opponentScore.Text = WordGame.scoreOpponent.ToString();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            (App.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            e.Cancel = true;
        }
    }
}

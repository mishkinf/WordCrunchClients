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
using WordCruncherWP7.Messages;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Tasks;
using WordCruncherWP7.CrunchEventArgs;

namespace WordCruncherWP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        CruncherClient c = new CruncherClient("10.211.55.4", 2222);
        bool inited = false;

        //CruncherClient c = new CruncherClient("ec2-184-73-99-238.compute-1.amazonaws.com", 2222);
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            MatchRequestMessage m = new MatchRequestMessage();
            m.encode();
            CrunchCore.OnGameStart += new EventHandler(CrunchCore_OnGameStart);
            CrunchCore.OnGameEnd += new EventHandler(CrunchCore_OnGameEnd);
            CrunchCore.OnError += new EventHandler<ErrorArgs>(CrunchCore_OnError);
        }

        void CrunchCore_OnError(object sender, ErrorArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                if (!Constants.ErrorFlagged)
                {
                    MessageBox.Show(e.errorMessage);
                    Constants.ErrorFlagged = true;
                }

            });
        }

        void CrunchCore_OnGameEnd(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                NavigationService.Navigate(new Uri("/Pages/EndGame.xaml", UriKind.Relative));
            });
        }

        void CrunchCore_OnGameStart(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
            });
        }

        private void playGame(object sender, RoutedEventArgs e)
        {
            Constants.ErrorFlagged = false;
            CrunchCore.Setup();
            CrunchCore.inited = false;
            CrunchCore.Connect();
        }

        private void feedbackButton_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask review = new MarketplaceReviewTask();
            review.Show();
        }
    }
}
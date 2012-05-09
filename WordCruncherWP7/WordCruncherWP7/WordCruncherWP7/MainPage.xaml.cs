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
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;

namespace WordCruncherWP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        bool inited = false;

        public MainPage()
        {
            InitializeComponent();

            MatchRequestMessage m = new MatchRequestMessage();

            CrunchCore.OnGameStart += new EventHandler(CrunchCore_OnGameStart);
            CrunchCore.OnGameEnd += new EventHandler(CrunchCore_OnGameEnd);
            CrunchCore.OnError += new EventHandler<ErrorArgs>(CrunchCore_OnError);
            CrunchCore.OnCreateConnectionCompleted += new EventHandler<ConnectionArgs>(CrunchCore_OnCreateConnectionCompleted);

            if (Globals.AppSettings.FirstRun || Globals.AppSettings.Username == null)
                Dispatcher.BeginInvoke(() =>
                {
                    Globals.AppSettings.FirstRun = false;
                    NavigationService.Navigate(new Uri("/Pages/InitialSetup.xaml", UriKind.Relative));
                });
            else
                Globals.YourUsername = Globals.AppSettings.Username;

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                CrunchCore.Disconnect();
            }
            catch
            {
            }

            base.OnNavigatedTo(e);
        }

        void CrunchCore_OnCreateConnectionCompleted(object sender, ConnectionArgs e)
        {
            CrunchCore.SendMessage(new MatchRequestMessage());
        }

        void CrunchCore_OnError(object sender, ErrorArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                if (!Globals.ErrorFlagged)
                {
                    MessageBox.Show(e.errorMessage);
                    Globals.ErrorFlagged = true;
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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
            WordGame.ResetGame();

            Globals.ErrorFlagged = false;

            CrunchCore.inited = false;
            CrunchCore.Connect();

            Dispatcher.BeginInvoke(() =>
            {
                NavigationService.Navigate(new Uri("/Pages/LoadingGame.xaml", UriKind.Relative));
            });
        }

        private void feedbackButton_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask review = new MarketplaceReviewTask();
            review.Show();
        }

        private void thumpJumpLogo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowWebsite();
        }

        private void instructionsBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            instructionsPanel.Visibility = Visibility.Visible;
        }

        private void ShowWebsite()
        {
            WebBrowserTask task = new WebBrowserTask();
            task.URL = "http://www.thumpjump.com/wp7/mobile.html";
            task.Show();
        }

        private void instructionsPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            instructionsPanel.Visibility = Visibility.Collapsed;
        }
    }
}
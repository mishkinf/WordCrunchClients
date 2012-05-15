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
    public partial class PausedGame : PhoneApplicationPage
    {
        public PausedGame()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            GoBackToGame();
            e.Cancel = true;
        }

        private void GoBackToGame()
        {
            (App.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void mainMenuBtn_Copy_Click(object sender, RoutedEventArgs e)
        {
            GoBackToGame();
        }

        private void mainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}

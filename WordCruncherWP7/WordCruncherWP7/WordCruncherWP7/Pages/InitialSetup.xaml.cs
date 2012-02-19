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
    public partial class InitialSetup : PhoneApplicationPage
    {
        public InitialSetup()
        {
            InitializeComponent();
            usernameBox.Focus();
        }

        private bool ValidUsername(String username)
        {
            //CrunchCore.
            return true;
        }

        private void goButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	if (ValidUsername(usernameBox.Text))
            {
                Globals.AppSettings.Username = usernameBox.Text;
                Globals.YourUsername = usernameBox.Text;

                Dispatcher.BeginInvoke(() =>
                {
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                });
            }
            else
            {
                errorMessage.Visibility = Visibility.Visible;
            }
        }
    }
}

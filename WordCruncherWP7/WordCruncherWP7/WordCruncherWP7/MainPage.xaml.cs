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

namespace WordCruncherWP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        CruncherClient c = new CruncherClient("10.211.55.2", 2222);
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            MatchRequestMessage m = new MatchRequestMessage();
            m.encode();
        }

        private void playGame(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void textConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            //CruncherClient c = new CruncherClient("ec2-184-73-99-238.compute-1.amazonaws.com", 2222);
            //CruncherClient c = new CruncherClient("10.211.55.2", 2222);

            c.OnCreateConnectionCompleted += new EventHandler<ConnectionArgs>(c_CreateConnectionCompleted);
            c.Connect();
            
        }

        private void c_CreateConnectionCompleted(object sender, ConnectionArgs e)
        {
            c.SendMessage(new HelloMessage("mishkin"));

            Dispatcher.BeginInvoke( () => { 
                //your ui update code 
                if (e.ConnectionOk)
                    connectionResult.Text = "Successfully Connected!";
                else
                    connectionResult.Text = "Nope.";
            });
        }
    }
}
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

namespace WordCruncherWP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        CruncherClient c = new CruncherClient("10.211.55.2", 2222);
        bool inited = false;

        //CruncherClient c = new CruncherClient("ec2-184-73-99-238.compute-1.amazonaws.com", 2222);
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            MatchRequestMessage m = new MatchRequestMessage();
            m.encode();
            CrunchCore.OnGameStart += new EventHandler(CrunchCore_OnGameStart);

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
            CrunchCore.Setup();
        }

        private void textConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            //CruncherClient c = new CruncherClient("ec2-184-73-99-238.compute-1.amazonaws.com", 2222);
            //CruncherClient c = new CruncherClient("10.211.55.2", 2222);

            c.OnCreateConnectionCompleted += new EventHandler<ConnectionArgs>(c_CreateConnectionCompleted);
            c.OnDataReceivedSuccessfully += new EventHandler<MessageArgs>(c_OnDataReceivedSuccessfully);
            c.Connect();
            
        }

        void c_OnDataReceivedSuccessfully(object sender, MessageArgs e)
        {
            JObject msg = JObject.Parse(e.Message);

            string msg_type = (string)msg["type"];

              //players: ['user1', 'user2'],
              //bomb_count: 3, // the starting number of bombs each player has
              //board: {
              //  columns: 5,
              //  rows: 5,
              //  matrix: [

              //    // Arrays containing the data for a square in the following order:
              //    // [(ascii code for lowercase letter),(point value of the tile)] - currently only has letter data
              //    [84,4],
              //    [92,1],
              //    [89,1],

              //    // 25 (5 * 5) total squares in the matrix
              //    ...
              //  ]

            switch (msg_type)
            {
                case "hello":
                    c.SendMessage(new MatchRequestMessage());
                    break;
                case "in_queue":
                    // Do nothing.. Display waiting message to user
                    break;
                case "start_game":
                    WordGame.InitGame(550, 800);
                    StartGameMessage s = new StartGameMessage();
                    s.fromJSON(e.Message);

                    Dispatcher.BeginInvoke(() =>
                    {
                        NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
                    });
                    break;
            }
            //if((string)o["type"] == "hi")
            //    int i = 0;
        }

        private void c_CreateConnectionCompleted(object sender, ConnectionArgs e)
        {
            if(!inited)
                c.SendMessage(new HelloMessage("mishkin"));
            inited = true;

            //Dispatcher.BeginInvoke( () => { 
            //    //your ui update code 
            //    if (e.ConnectionOk)
            //        connectionResult.Text = "Successfully Connected!";
            //    else
            //        connectionResult.Text = "Nope.";
            //});
        }


    }
}
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
using Newtonsoft.Json.Linq;
using WordCruncherWP7.Messages;
using System.Windows.Threading;
using System.Windows.Navigation;

namespace WordCruncherWP7
{
    public static class CrunchCore
    {
        static CruncherClient c = new CruncherClient("10.211.55.2", 2222);
        static bool inited = false;
        public static event EventHandler OnGameStart;
        public static event EventHandler OnScoreChange;
        public static event EventHandler<BombedArgs> OnBombed;
        //static CruncherClient c = new CruncherClient("ec2-184-73-99-238.compute-1.amazonaws.com", 2222);

        public class BombedArgs : EventArgs
        {
            public int[] bomd_indices;

            public BombedArgs(int[] indices)
            {
                bomd_indices = indices;
            }
        }

        public static void Setup()
        {
            c.OnCreateConnectionCompleted += c_CreateConnectionCompleted;
            c.OnDataReceivedSuccessfully += c_OnDataReceivedSuccessfully;
            c.Connect();
        }

        public static void SendMessage(iMessage message)
        {
            c.SendMessage(message);
        }

        static void c_OnDataReceivedSuccessfully(object sender, MessageArgs e)
        {
            JObject msg = JObject.Parse(e.Message);

            string msg_type = (string)msg["type"];

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

                    if (OnGameStart != null)
                        OnGameStart("CrunchCore", new RoutedEventArgs());
                    break;
                case "good_guess_response":
                    GoodGuessResponse response = new GoodGuessResponse();
                    response.fromJSON(e.Message);

                    WordGame.player1Score = response.scores[0];
                    WordGame.player2Score = response.scores[1];

                    if (OnScoreChange != null)
                        OnScoreChange("Crunch Core", new RoutedEventArgs());
                    break;
                case "bombed_guess_response":
                    BombedGuessResponse bomb = new BombedGuessResponse();
                    bomb.fromJSON(e.Message);

                    RoutedEventArgs args = new RoutedEventArgs();
                    
                    if (OnBombed != null)
                        OnBombed("CrunchCore", new BombedArgs(bomb.bombs));

                    break;
                case "end_game":
                    break;
            }
            //if((string)o["type"] == "hi")
            //    int i = 0;
        }

        private static void c_CreateConnectionCompleted(object sender, ConnectionArgs e)
        {
            if (!inited)
                c.SendMessage(new HelloMessage("mishkin"));
            inited = true;

            //Dispatcher.BeginInvoke(() =>
            //{
               
            //});
        }
    }
}

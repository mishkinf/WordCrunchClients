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
using Microsoft.Phone.Controls;
using WordCruncherWP7.CrunchEventArgs;
using Microsoft.Phone.Shell;
using System.Collections.Generic;
using System.Threading;

namespace WordCruncherWP7
{
    public static class CrunchCore
    {
        static CruncherClient c;
        public static bool inited = false;
        public static event EventHandler OnGameStart;
        public static event EventHandler OnGameEnd;
        public static event EventHandler OnScoreChange;
        public static event EventHandler<ErrorArgs> OnError;
        public static event EventHandler<BombedArgs> OnBombed;
        public static event EventHandler<GoodGuessArgs> OnGoodGuess;
        public static event EventHandler<HighScoresArgs> OnHighScoresReturned;
        public static event EventHandler<ConnectionArgs> OnCreateConnectionCompleted;
        public static List<Word> yourWords = new List<Word>();
        public static List<Word> opponentsWords = new List<Word>();
        static DispatcherTimer timer = new DispatcherTimer();
        static int time = 0;

        private static void Setup()
        {
            c = new CruncherClient(Globals.ServerURL, System.Convert.ToInt32(Globals.ServerPort));


            c.OnCreateConnectionCompleted += c_CreateConnectionCompleted;
            c.OnDataReceivedSuccessfully += c_OnDataReceivedSuccessfully;
            timer.Tick += (object o, EventArgs sender) =>
            {
                if (time++ > 20 && OnError != null)
                {
                    OnError("CrunchCore", new ErrorArgs("We're sorry, we seem to be experiencing technical difficulties! Try again later."));
                    StopTimer();
                }
            };
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0); // 100 Milliseconds 
        }

        public static void StopTimer()
        {
            timer.Stop();
            time = 0;
        }

        public static void Disconnect()
        {
            c.Disconnect();
        }

        public static void Connect()
        {
            if (c != null)
            {
                c.OnCreateConnectionCompleted -= c_CreateConnectionCompleted;
                c.OnDataReceivedSuccessfully -= c_OnDataReceivedSuccessfully;
            }

            Setup();
            c.Connect();
            time = 0;
            timer.Start();
        }

        public static void SendMessage(iEncodableMessage message)
        {
            c.SendMessage(message);
        }

        static void c_OnDataReceivedSuccessfully(object sender, MessageArgs e)
        {
            try
            {
                JObject msg = JObject.Parse(e.Message);

                string msg_type = (string)msg["type"];

                switch (msg_type)
                {
                    case "wc_hello":

                        break;
                    case "wc_hello_response":
                        if (OnCreateConnectionCompleted != null)
                            OnCreateConnectionCompleted("CrunchCore", new ConnectionArgs(true));
                        
                        break;
                    case "wc_in_queue":
                        // Do nothing.. Display waiting message to user
                        break;
                    case "wc_start_game":
                        WordGame.InitGame(500, 800);
                        StartGameMessage s = StartGameMessage.fromJSON(e.Message);

                        if (Int32.Parse(s.player_id) == 0)
                        {
                            Globals.PlayerIndex = 0;
                            //Globals.YourUsername
                            Globals.OpponentUsername = s.user2;
                        }
                        else
                        {
                            Globals.PlayerIndex = 1;
                            Globals.OpponentUsername = s.user1;
                        }

                        if (OnGameStart != null)
                        {
                            OnGameStart("CrunchCore", new RoutedEventArgs());
                            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                        }
                        break;
                    case "wc_good_guess":
                        GoodGuessResponseMessage response = GoodGuessResponseMessage.fromJSON(e.Message);

                        WordGame.scoreYou = response.scoreYou;
                        WordGame.scoreOpponent = response.scoreOpponent;

                        if (OnScoreChange != null)
                            OnScoreChange("Crunch Core", new RoutedEventArgs());

                        if (OnGoodGuess != null)
                            OnGoodGuess("CrunchCore", new GoodGuessArgs(response));

                        break;
                    case "wc_bombed_guess":
                        BombedGuessResponseMessage bomb = BombedGuessResponseMessage.fromJSON(e.Message);

                        RoutedEventArgs args = new RoutedEventArgs();

                        WordGame.scoreYou = bomb.scoreYou;
                        WordGame.scoreOpponent = bomb.scoreOpponent;

                        if (OnBombed != null)
                            OnBombed("CrunchCore", new BombedArgs(bomb));

                        break;
                    case "wc_stop_game":
                        c.Disconnect();
                        JObject json = JObject.Parse(e.Message);
                        string reason = (String)json.Property("reason").Value;

                        if (OnGameEnd != null)
                        {
                            OnGameEnd("CrunchCore", new EventArgs());
                        }
                        break;
                    case "wc_high_scores":
                        HighScoresResponseMessage scoresResponse = HighScoresResponseMessage.fromJSON(e.Message);

                        if (OnHighScoresReturned != null)
                            OnHighScoresReturned("CrunchCore", new HighScoresArgs());
                        break;
                    case "wc_goodbye":
                        
                        c.Disconnect();

                        if (OnError != null)
                            OnError("CrunchCore", new ErrorArgs((string)msg["reason"]));

                        break;
                }
            }
            catch (Exception ex)
            {
                c.Disconnect();
                if (OnError != null)
                    OnError("CrunchCore", new ErrorArgs(e.Message));
            }
        }

        private static void c_CreateConnectionCompleted(object sender, ConnectionArgs e)
        {
            if (!inited)
                c.SendMessage(new HelloMessage());
            inited = true;
        }
    }
}

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

namespace WordCruncherWP7
{
    public static class CrunchCore
    {
        static CruncherClient c = new CruncherClient(Constants.ServerURL, System.Convert.ToInt32(Constants.ServerPort));
        public static bool inited = false;
        public static event EventHandler OnGameStart;
        public static event EventHandler OnGameEnd;
        public static event EventHandler OnScoreChange;
        public static event EventHandler<ErrorArgs> OnError;
        public static event EventHandler<BombedArgs> OnBombed;
        public static event EventHandler<GoodGuessArgs> OnGoodGuess;
        public static event EventHandler<HighScoresArgs> OnHighScoresReturned;
        public static List<Word> yourWords = new List<Word>();
        public static List<Word> opponentsWords = new List<Word>();

        public static void Setup()
        {
            c.OnCreateConnectionCompleted += c_CreateConnectionCompleted;
            c.OnDataReceivedSuccessfully += c_OnDataReceivedSuccessfully;

        }

        public static void Connect()
        {
            c.Connect();
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
                    case "hello":

                        break;
                    case "hello_response":
                        c.SendMessage(new MatchRequestMessage());
                        break;
                    case "in_queue":
                        // Do nothing.. Display waiting message to user
                        break;
                    case "start_game":
                        WordGame.InitGame(500, 800);
                        StartGameMessage s = StartGameMessage.fromJSON(e.Message);

                        if (s.player1 == Constants.username)
                            Constants.PlayerIndex = 1;
                        else if (s.player2 == Constants.username)
                            Constants.PlayerIndex = 2;

                        if (OnGameStart != null)
                        {
                            OnGameStart("CrunchCore", new RoutedEventArgs());
                            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                        }
                        break;
                    case "good_guess_response":
                        GoodGuessResponseMessage response = GoodGuessResponseMessage.fromJSON(e.Message);

                        WordGame.scoreYou = response.scoreYou;
                        WordGame.scoreOpponent = response.scoreOpponent;

                        if (OnScoreChange != null)
                            OnScoreChange("Crunch Core", new RoutedEventArgs());

                        if (OnGoodGuess != null)
                            OnGoodGuess("CrunchCore", new GoodGuessArgs(response));

                        break;
                    case "bombed_guess_response":
                        BombedGuessResponseMessage bomb = BombedGuessResponseMessage.fromJSON(e.Message);

                        RoutedEventArgs args = new RoutedEventArgs();

                        WordGame.scoreYou = bomb.scoreYou;
                        WordGame.scoreOpponent = bomb.scoreOpponent;

                        if (OnBombed != null)
                            OnBombed("CrunchCore", new BombedArgs(bomb));

                        break;
                    case "end_game":
                        JObject json = JObject.Parse(e.Message);
                        string reason = (String)json.Property("reason").Value;

                        if (OnGameEnd != null)
                        {
                            OnGameEnd("CrunchCore", new EventArgs());
                        }
                        break;
                    case "high_scores_response":
                        HighScoresResponseMessage scoresResponse = HighScoresResponseMessage.fromJSON(e.Message);

                        if (OnHighScoresReturned != null)
                            OnHighScoresReturned("CrunchCore", new HighScoresArgs(scoresResponse.matchResults));
                        break;
                    case "goodbye":
                        break;
                }
            }
            catch (Exception ex)
            {
               
                if (OnError != null)
                    OnError("CrunchCore", new ErrorArgs(ex.Message));
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

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
using System.Collections.ObjectModel;
using WordCruncherWP7.Messages;
using Microsoft.Phone.Reactive;
using System.Runtime.CompilerServices;

namespace WordCruncherWP7
{
    public partial class HighScores : PhoneApplicationPage
    {
        public HighScores()
        {
            InitializeComponent();
            this.highScoresListBox.ItemsSource = Globals.MatchResults;
            //this.highScoresListBox.
            CrunchCore.OnHighScoresReturned += new EventHandler<CrunchEventArgs.HighScoresArgs>(CrunchCore_OnHighScoresReturned);
            CrunchCore.OnCreateConnectionCompleted += new EventHandler<ConnectionArgs>(CrunchCore_OnCreateConnectionCompleted);
            CrunchCore.inited = false;
            CrunchCore.Connect();  
        }

        void CrunchCore_OnCreateConnectionCompleted(object sender, ConnectionArgs e)
        {
            CrunchCore.SendMessage(new HighScoresMessage());
        }

        void CrunchCore_OnHighScoresReturned(object sender, CrunchEventArgs.HighScoresArgs e)
        {
           // test();
            Dispatcher.BeginInvoke(() =>
            {

                //this.highScoresListBox.Visibility = System.Windows.Visibility.Collapsed;
                //this.highScoresListBox.Visibility = System.Windows.Visibility.Visible;
                //ItemCollection itemCol;

                List<MatchResult> results = new List<MatchResult>();
                foreach (MatchResult result in Globals.MatchResults)
                    results.Add(result);
                this.highScoresListBox.ItemsSource = results;
                test();
            });

            //this.highScoresListBox.ItemsSource = Globals.MatchResults;
            //this.matchResults = e.matchResults;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void test()
        {
            highScoresListBox.Opacity = 0.0;

            for (int i = 0; i < highScoresListBox.Items.Count; i++)
            {
                ListBoxItem item = (ListBoxItem)(highScoresListBox).ItemContainerGenerator.ContainerFromIndex(i);
                if (item != null)
                {
                    VisualStateManager.GoToState(item, "BeforeLoaded", false);
                }
            }

            (highScoresListBox).Opacity = 1.0;

            var observable = from t in Observable.Interval(TimeSpan.FromMilliseconds(200))
                                     .TimeInterval().Take((highScoresListBox).Items.Count)
                             select t.Value;

            observable.Subscribe(i => Dispatcher.BeginInvoke(() =>
                VisualStateManager.GoToState(
                    (ListBoxItem)(highScoresListBox).ItemContainerGenerator
                        .ContainerFromIndex((int)i), "AfterLoaded", true)));
        }

        private void highScoresListBox_Loaded(object sender, RoutedEventArgs e)
        {
            //test();  
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                this.highScoresListBox.ItemsSource = Globals.MatchResults;
                test();
            });
        }


    }
}

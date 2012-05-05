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
using System.Collections.Generic;

namespace WordCruncherWP7
{
    public static class Globals
    {
        //  "10.37.129.2";
        public const String ServerURL = "ec2-184-73-99-238.compute-1.amazonaws.com"; // "10.211.55.2"; //"192.168.1.4"; //ec2-184-73-99-238.compute-1.amazonaws.com";
        public const String ServerPort = "2222";
        public const int ProtocolVersion = 1;

        public static bool ErrorFlagged = false;
        public static String YourUsername;
        public static String OpponentUsername;
        public static int PlayerIndex = -1;
        public static List<MatchResult> MatchResults = new List<MatchResult>();
        public static ApplicationSettings AppSettings = new ApplicationSettings();

        public static void Reset()
        {
            MatchResults.Clear();            
        }
    }
}

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
    public static class Constants
    {
        //  "10.37.129.2";
        public static String ServerURL = "ec2-184-73-99-238.compute-1.amazonaws.com";
        public static String ServerPort = "2222";
        public static int ProtocolVersion = 1;
        public static bool ErrorFlagged = false;
        public static String username = "Mishkin2";
        public static int PlayerIndex = -1;
        public static List<MatchResult> MatchResults = new List<MatchResult>();
    }
}

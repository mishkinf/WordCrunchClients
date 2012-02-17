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

namespace WordCruncherWP7.Messages
{
    public class HighScoresMessage : iMessage, iEncodableMessage
    {
        public HighScoresMessage()
        {
        }

        public String encode()
        {
            JObject json = new JObject(
                new JProperty("type", "high_scores")
                );

            return json.ToString();
        }
    }
}

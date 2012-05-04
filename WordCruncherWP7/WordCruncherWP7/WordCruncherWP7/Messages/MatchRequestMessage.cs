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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WordCruncherWP7.Messages
{
    public class MatchRequestMessage : iMessage, iEncodableMessage
    {
        public MatchRequestMessage()
        {
        }

        public String encode() {
            JObject o = new JObject(new JProperty("type", "wc_match_request"));

            return o.ToString();
        }
    }
}

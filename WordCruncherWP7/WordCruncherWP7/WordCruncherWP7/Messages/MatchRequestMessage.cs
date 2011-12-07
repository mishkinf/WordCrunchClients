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
    public class MatchRequestMessage : iMessage
    {
        public MatchRequestMessage()
        {
        }

        public static int TYPE_CODE = 0x0002;

        public void fromJSON(string message)
        {
            JObject o = JObject.Parse(message);

        }

        public String encode() {
            JObject o = new JObject(new JProperty("type", "match_request"));

            return o.ToString();
        }
    }
}

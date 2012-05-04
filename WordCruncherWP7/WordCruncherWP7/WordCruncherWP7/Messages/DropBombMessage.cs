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
    public class DropBombMessage : iMessage, iEncodableMessage
    {
        public int index;

        public static DropBombMessage fromJson(JObject json) {
            return new DropBombMessage((int)json.Property("index").Value);
        }

        public DropBombMessage(int index)
        {
            this.index = index;
        }

        public String encode() {
            JObject json = new JObject(
                new JProperty("type", "wc_drop_bomb"),
                new JProperty("index", index)
                );
      
            return json.ToString();
        }
    }
}

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
    public class DropWallMessage : iMessage, iEncodableMessage
    {
        public int index1, index2;

        public static DropWallMessage fromJson(JObject json) {
            return new DropWallMessage((int)json.Property("index1").Value, (int)json.Property("index2").Value);
        }

        public DropWallMessage(int index1, int index2)
        {
            this.index1 = index1;
            this.index2 = index2;
        }

        public String encode() {
            JObject json = new JObject(
                new JProperty("type", "drop_wall"),
                new JProperty("index1", index1),
                new JProperty("index2", index2)
                );
      
            return json.ToString();
        }
    }
}

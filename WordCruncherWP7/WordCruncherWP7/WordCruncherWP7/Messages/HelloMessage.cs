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
    public class HelloMessage : iMessage
    {
        public static int TYPE_CODE = 0x0001;

        public String username;
        public String token;
        public int version;

        public void fromJSON(string message)
        {
            JObject json = JObject.Parse(message);
            this.username = (String)json.Property("username").Value;
            this.token = (String)json.Property("token").Value;
            this.version = (int)json.Property("version").Value;
        }

        public HelloMessage(String username, String token, int version) {
            this.username = username;
            this.token = token;
            this.version = version;
        }

        public String encode() {
            JObject json = new JObject(
                new JProperty("type", "hello"),
                new JProperty("username", this.username)
                );
      
            return json.ToString();
        }
    }
}

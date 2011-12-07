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
    public class HelloMessage : Message
    {
        public static int TYPE_CODE = 0x0001;

        public String username;

        public static HelloMessage fromJson(JObject json) {
            return new HelloMessage((String) json.Property("username").Value);
        }

        public HelloMessage(String username) {
            this.username = username;
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

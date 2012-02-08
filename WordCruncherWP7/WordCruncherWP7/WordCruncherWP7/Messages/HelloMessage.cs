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
using Microsoft.Phone.Info; 

namespace WordCruncherWP7.Messages
{
    public class HelloMessage : iMessage, iEncodableMessage
    {
        public String username;
        public String token;
        public int version;

        //public static HelloMessage fromJSON(string message)
        //{
        //    JObject json = JObject.Parse(message);
        //    String username = (String)json.Property("username").Value;
        //    String token = (String)json.Property("token").Value;
        //    int version = (int)json.Property("version").Value;

        //    return new HelloMessage(usename, token, version);
        //}

        public HelloMessage(String username) {
            this.username = username;
        }

        public String encode() {
            string deviceUniqueID = string.Empty;
            object deviceID;
            if (DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out deviceID))
            {
                deviceUniqueID =  Convert.ToBase64String((byte[])deviceID);
                Constants.username = deviceUniqueID;
            }

            if (deviceUniqueID == null || deviceUniqueID == "")
                deviceUniqueID = "FAKEUNIQUEID";


            JObject json = new JObject(
                new JProperty("type", "hello"),
                new JProperty("username", deviceUniqueID),
                new JProperty("token", Constants.username),
                new JProperty("version", Constants.ProtocolVersion)
                );
      
            return json.ToString();
        }
    }
}

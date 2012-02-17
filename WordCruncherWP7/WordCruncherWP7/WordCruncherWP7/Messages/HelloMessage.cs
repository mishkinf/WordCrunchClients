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
        public String token;
        public int version;

        public HelloMessage() {
        }

        public String encode() {
            string deviceUniqueID = string.Empty;
            object deviceID;

            if (DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out deviceID))
            {
                deviceUniqueID = Convert.ToBase64String((byte[])deviceID);
            }

            if (deviceUniqueID == null || deviceUniqueID == "")
                deviceUniqueID = "FAKEUNIQUEID";

            JObject json = new JObject(
                new JProperty("type", "hello"),
                new JProperty("username", Globals.YourUsername),
                new JProperty("token", deviceUniqueID),
                new JProperty("version", Globals.ProtocolVersion)
                );
      
            return json.ToString();
        }
    }
}

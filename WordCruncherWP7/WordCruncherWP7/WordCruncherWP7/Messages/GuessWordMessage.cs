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
using Newtonsoft.Json;

namespace WordCruncherWP7.Messages
{
    public class GuessWordMessage : iMessage, iEncodableMessage
    {
        private static Random rand = new Random();
        private static int guessId = 0;

        public int id;
        private String encoded;

        public GuessWordMessage(int[] selection) {
            id = guessId++;

            JObject o = new JObject(new JProperty("type", "guess_word"));
            o.Add(new JProperty("id", id));
            o.Add(new JProperty("selection", selection));

            encoded = o.ToString();
        }

        public String encode() {
            return encoded;
        }
    }
}

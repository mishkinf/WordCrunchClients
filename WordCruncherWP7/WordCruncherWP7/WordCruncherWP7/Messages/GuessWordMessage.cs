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
    public class GuessWordMessage : Message
    {
        /*
        public static int TYPE_CODE = 0x0005;
        private static Random rand = new Random();
        private static int guessId = 0;

        public int id;
        private String encoded;

        public GuessWordMessage(SelectedSquare[] selection) {
            JObject json = new JObject();
            id = guessId++;

            json.put("type", "guess_word");
            json.put("id", id);
            JArray selectionJson = new JArray();
            json.put("selection", selectionJson);

            for(int i = 0; i < selection.length; i++) {
                SelectedSquare selectedSquare = selection[i];

                selectionJson.add(selectedSquare.index + 1);
            }

            encoded = json.ToString();
        }

        public String encode() {
            return encoded;
        }*/

        public String encode() { return ""; }
    }
}

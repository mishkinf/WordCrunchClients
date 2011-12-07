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
    public class StartGameMessage : Message
    {
        /*public static int TYPE_CODE = 0x0004;

        public Board board;
        public int bombCount;
        public String[] players;

        public static StartGameMessage fromJson(JSONObject json) {
            JSONObject jsonBoard = (JSONObject) json.get("board");
            Board board = Board.fromJson(jsonBoard);
            int bombCount = ((Long) json.get("bomb_count")).intValue();

            JSONArray playersJson = (JSONArray) json.get("players");
            int length = playersJson.size();
            String[] players = new String[length];
            for(int i = 0; i < length; i++) {
                players[i] = (String) playersJson.get(i);
            }

            return new StartGameMessage(board, players, bombCount);
        }

        public StartGameMessage(Board board, String[] players, int bombCount) {
            this.board = board;
            this.players = players;
            this.bombCount = bombCount;
        }

        public int index(String username) {
            int length = players.length;
            for(int i = 0; i < length; i++) {
                if(players[i].equals(username)) {
                    return i;
                }
            }

            return -1;
        }
         

        public String encode() {
            JObject json = new JObject(
                new JProperty("type", "hello"),
                new JProperty("username", this.username)
            );

            return json.ToString();
        }*/

        public String encode() { return "";  }
    }
}

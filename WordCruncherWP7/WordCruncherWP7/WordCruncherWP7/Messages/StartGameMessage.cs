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
using System.Collections.Generic;

namespace WordCruncherWP7.Messages
{
    public class StartGameMessage : iMessage
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
        }
         
          players: ['user1', 'user2'],
          bomb_count: 3, // the starting number of bombs each player has
          board: {
            columns: 5,
            rows: 5,
            matrix: [

              // Arrays containing the data for a square in the following order:
              // [(ascii code for lowercase letter),(point value of the tile)] - currently only has letter data
              [84,4],
              [92,1],
              [89,1],

              // 25 (5 * 5) total squares in the matrix
              ...
            ]
         */

        public void fromJSON(string message)
        {
            JObject o = JObject.Parse(message);

            string player1 = (string)o["players"][0];
            string player2 = (string)o["players"][1];
            int bomb_count = (int)o["bomb_count"];

            int columns = (int)o["board"]["columns"];
            int rows = (int)o["board"]["rows"];

            int total = columns*rows;
            List<GameSquare> squares = new List<GameSquare>();

            WordGame.SetWordGameSize(columns, rows);

            int index = 0;
            for(int c = 0; c < columns; c++)
            {
                for(int r = 0; r < rows; r++) {
                    int letter_ascii = (int)o["board"]["matrix"][index][0];
                    string letter = System.Convert.ToChar(letter_ascii).ToString();
                    int value = (int)o["board"]["matrix"][index][1];

                    WordGame.SetSquare(letter, value, r, c, index);
                
                    index++;
                }
            }

            int e = 0;
        }

        public String encode() { return "";  }
    }
}

//using System;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;

//namespace WordCruncherWP7.Messages
//{
//    public class Class1
//    {

//    }
//}


//## hello - responses(hello_response, goodbye)
//{
//  type: "hello",
//  username: "chromaticbum" // requested username
//  token: "adlk23lk", // unique token identifying the user
//  version: 1 // the version protocol of the client
//}

//## request_match - responses(in_queue, start_game)
//{
//  type: "request_match"
//}

//## high_scores - responses(high_scores_response)
//{
//  type: "high_scores"
//}

//## guess_word - responses(good_guess, bad_guess)
//{
//  type: "guess_word",
//  id: 123124512, // unique id for this guess

//  // selection is an array of matrix indexes used to select the word
//  selection: [0,1,2,3]
//}

//## drop_bomb - no responses
//- lays a bomb at the given index (if the player has any bombs left)
//{
//  type: "drop_bomb",
//  index: 3 // index on the matrix where to drop the bomb
//}

//Messages for Server to Client

//## hello_response - no responses
//- confirms client hello request

//{
//  type: "hello_response"
//}

//## high_scores_response - no responses
//{
//  type: "high_scores_response",
//  players: ["player1", "player2"],
//  scores: [[22,33], [44,33]]
//}

//## in_queue - no responses
//- indicates the the player is waiting for another player
//- to request a match

//{
//  type: "in_queue"
//}

//## start_game - no response
//- indicates that there is another player ready to play as well

//{
//  type: "start_game",

//  // list of players in the game (order used for score keeping in guess_word_response)
//  players: ['user1', 'user2'],
//  bomb_count: 3, // the starting number of bombs each player has
//  board: {
//    columns: 5,
//    rows: 5,
//    matrix: [

//      // Arrays containing the data for a square in the following order:
//      // [(ascii code for lowercase letter),(point value of the tile)] - currently only has letter data
//      [84,4],
//      [92,1],
//      [89,1],

//      // 25 (5 * 5) total squares in the matrix
//      ...
//    ]
//  }
//}

//## good_guess_response
//{
//  type: "good_guess_response",
//  // index is determined from the start_game message
//  // your player index is equal to the index at which
//  // your name shows up in the list of players
//  player_index: 1, // the index of the player who got the word
//  id: 213214124, // the id of the guess_word message
//  selection: [1,2,3,4] // the selection path used to generate the word
//  scores: [0,3] // updated player scores
//}

//## bombed_guess_response
//- returned if the selection made had an enemy bomb(s) on it
//{
//  type: "bombed_guess_response",
//  player_index: 2 // the index of the player who guessed the bombed word
//  id: 321312321, // id of the guess_word message
//  selection: [1,2,3,4] // selection path
//  bombs: [2,4], // indexes of where the bombs were
//  scores: [23, 17] // updated game scores
//}

//## bad_guess_response
//{
//  type: "bad_guess_response",
//  id: 324,
//  reason: "fake" // fake, blocked, or disabled
//}

//## end_game - no response
//{
//  type: "end_game",
//  reason: "timeout"
//}
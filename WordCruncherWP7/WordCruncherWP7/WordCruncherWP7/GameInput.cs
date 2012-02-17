using System;
using System.Net;
using System.Windows;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using WordCruncherWP7.Messages;
using System.Collections.Generic;

namespace WordCruncherWP7
{
    public class GameInput
    {
        public void Update(TouchCollection curTouches)
        {
            foreach (TouchLocation location in curTouches)
            {
                float fingerX = location.Position.X;
                float fingerY = location.Position.Y;

                switch (location.State)
                {
                    case TouchLocationState.Pressed:
                        foreach (GameSquare gs in WordGame.squares)
                            if (gs.collisionRect.Contains((int)fingerX, (int)fingerY))
                            {
                                WordGame.Selecting = true;
                                gs.color = Color.Gray;
                                WordGame.selectedSquares.Add(gs);
                            }
                        break;
                    case TouchLocationState.Released:
                        int[] selectedIndexes = new int[WordGame.selectedSquares.Count];
                        int index = 0;
                        foreach (GameSquare gs in WordGame.selectedSquares)
                            selectedIndexes[index++] = gs.index + 1;

                        CrunchCore.SendMessage(new GuessWordMessage(selectedIndexes));

                        foreach (GameSquare gs in WordGame.squares)
                            gs.color = Color.Blue;

                        WordGame.Selecting = false;
                        WordGame.selectedSquares = new List<GameSquare>();
                        break;
                    case TouchLocationState.Moved:
                        if (WordGame.Selecting)
                        {
                            foreach (GameSquare gs in WordGame.squares)
                            {
                                if (gs.collisionRect.Contains((int)fingerX, (int)fingerY))
                                {
                                    if (WordGame.selectedSquares.Count > 0 && !WordGame.selectedSquares.Contains(gs) &&
                                        WordGame.IsValidNextSquare(WordGame.selectedSquares[WordGame.selectedSquares.Count-1], gs))
                                    {
                                        gs.color = Color.Gray;
                                        WordGame.selectedSquares.Add(gs);
                                    }
                                    else
                                    {
                                        if (WordGame.selectedSquares.Count - 2 >= 0 &&
                                            WordGame.selectedSquares[WordGame.selectedSquares.Count - 2] == gs)
                                        {
                                            WordGame.selectedSquares[WordGame.selectedSquares.Count - 1].color = Color.Blue;
                                            WordGame.selectedSquares.RemoveAt(WordGame.selectedSquares.Count - 1);
                                        }
                                    }
                                }
                            }
                        }

                        break;
                }
            }
        }
    }
}

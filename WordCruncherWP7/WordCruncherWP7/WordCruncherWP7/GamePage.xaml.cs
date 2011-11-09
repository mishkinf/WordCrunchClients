﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace WordCruncherWP7
{
    public partial class GamePage : PhoneApplicationPage
    {
        GameTimer timer;
        ContentManager content;
        SpriteBatch spriteBatch;
        Texture2D textureBlue;
        Texture2D textureRed;
        SpriteFont letterFont;
        SpriteFont letterValueFont;
        SpriteFont labelsFont;

        private WordGame wordGame;
        private float scale = 0.01f;
        private double time = 0.0;

        public GamePage()
        {
            InitializeComponent();

            // Get the application's ContentManager
            content = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;
            wordGame = new WordGame(550, 800); //SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Width, SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Height);   
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            // TODO: use this.content to load your game content here
//            texture = content.Load<Texture2D>("redRect");
            textureBlue = content.Load<Texture2D>("square_blue");// Texture2D(SharedGraphicsDeviceManager.Current.GraphicsDevice, 1, 1);
//            textureBlue.SetData(new Color[] { Color.Blue });
            
            textureRed = content.Load<Texture2D>("square_red"); //new Texture2D(SharedGraphicsDeviceManager.Current.GraphicsDevice, 1, 1);
//            textureRed.SetData(new Color[] { Color.Red });

            letterFont = content.Load<SpriteFont>("CourierNew");
            letterValueFont = content.Load<SpriteFont>("squareValue");
            labelsFont = content.Load<SpriteFont>("labelsFont");

            // Start the timer
            timer.Start();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            // TODO: Add your update logic here

            // Move the sprite around.
            UpdateSprite(e);
        }

        void UpdateSprite(GameTimerEventArgs e)
        {
            //get the state of the touch panel
            TouchCollection curTouches = TouchPanel.GetState();

            // Process touch locations
            foreach (TouchLocation location in curTouches)
            {
                float fingerX = location.Position.X;
                float fingerY = location.Position.Y;

                switch (location.State)
                {
                    case TouchLocationState.Pressed:

                        foreach (GameSquare gs in wordGame.squares)
                            if (gs.collisionRect.Contains((int)fingerX, (int)fingerY))
                            {
                                wordGame.Selecting = true;
                                gs.color = Color.Red;
                                wordGame.selectedSquares.Add(gs);

                            }


                        break;
                    case TouchLocationState.Released:
                        //Don't care about released state in this demo
                        foreach (GameSquare gs in wordGame.squares)
                                gs.color = Color.Blue;

                        wordGame.Selecting = false;
                        wordGame.selectedSquares = new List<GameSquare>();
                        break;
                    case TouchLocationState.Moved:
                        if (wordGame.Selecting)
                        {
                            foreach (GameSquare gs in wordGame.squares)
                            {
                                if (gs.collisionRect.Contains((int) fingerX, (int) fingerY))
                                {
                                    if (wordGame.selectedSquares.Count > 0 && !wordGame.selectedSquares.Contains(gs) &&
                                        wordGame.IsValidNextSquare(wordGame.selectedSquares.Last(), gs))
                                    {
                                        gs.color = Color.Red;
                                        wordGame.selectedSquares.Add(gs);
                                    }
                                    else
                                    {
                                        if (wordGame.selectedSquares.Count - 2 >= 0 &&
                                            wordGame.selectedSquares[wordGame.selectedSquares.Count - 2] == gs)
                                        {
                                            wordGame.selectedSquares.Last().color = Color.Blue;
                                            wordGame.selectedSquares.RemoveAt(wordGame.selectedSquares.Count - 1);
                                        }
                                    }
                                }
                            }
                        }

                        break;
                }
            }

            if (scale < 1)
            {
                time++;
                scale = MathFunctions.CircularInOut(time, 0.0f, 1.0f, 50);
            }
        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            // Draw the sprite
            spriteBatch.Begin();
//            spriteBatch.Draw(texture, spritePosition, Color.White);

            
            foreach (GameSquare gs in wordGame.squares)
            {
                if(gs.color == Color.Blue)
                    spriteBatch.Draw(textureBlue, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.Blue, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), scale, SpriteEffects.None, 0.0f);
//                    spriteBatch.Draw(textureBlue, gs.rect, Color.Blue);

                if(gs.color == Color.Red)
                    spriteBatch.Draw(textureRed, new Vector2(gs.rect.X + gs.rect.Width/2, gs.rect.Y + gs.rect.Height/2), null, Color.Red, 0.0f, new Vector2(gs.rect.Width/2, gs.rect.Height/2), 0.8f, SpriteEffects.None, 0.0f);

//                    spriteBatch.Draw(textureRed, gs.rect, Color.Red);

                spriteBatch.DrawString(letterValueFont, gs.GetValue().ToString(), new Vector2(gs.rect.X, gs.rect.Y+5), Color.LightBlue);
                spriteBatch.DrawString(letterFont, gs.letter.ToString(), new Vector2(gs.rect.X + 40, gs.rect.Y + 25), Color.White);
            }

//            spriteBatch.DrawString(labelsFont, "Time Left? 1:34", new Vector2(80, 650), Color.Lavender);
//            spriteBatch.DrawString(labelsFont, "You: 120", new Vector2(10, 700), Color.Green);
            spriteBatch.DrawString(labelsFont, wordGame.GetLetters(), new Vector2(20, 750), Color.White);

            spriteBatch.End();
        }
    }
}
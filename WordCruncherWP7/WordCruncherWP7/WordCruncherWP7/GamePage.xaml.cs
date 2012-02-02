using System;
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
using WordCruncherWP7.Messages;
using Mishimation;

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

        private float scale = 1.0f;
        private double time = 0;

        bool bombing = false;
        int bomb_ticks = 50;
        int[] bombed_indices = {};

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
            //WordGame.InitGame(420, 800); //SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Width, SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Height);   
            CrunchCore.OnBombed += new EventHandler<CrunchCore.BombedArgs>(CrunchCore_OnBombed);

            //Animator.AddAnimation(new Animation(0.2f, 1.0f, WordGame.squares[0,0].ScaleProperty, 0.5f, Easings.easeOutBounce));
            //foreach (GameSquare s in WordGame.squares)
            //{
            //    s.scale = 0.2f;
            //}
        }


        void CrunchCore_OnBombed(object sender, CrunchCore.BombedArgs e)
        {
            bombing = true;
            bomb_ticks = 100;

            this.bombed_indices = e.bomd_indices;
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

            if (bomb_ticks < 1)
            {
                bomb_ticks = 100;
                bombing = false;
            }

            if(bombing)
                bomb_ticks--;

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

                        foreach (GameSquare gs in WordGame.squares)
                            if (gs.collisionRect.Contains((int) fingerX, (int) fingerY))
                            {
                                WordGame.Selecting = true;
                                gs.color = Color.Red;
                                WordGame.selectedSquares.Add(gs);

                            }


                        break;
                    case TouchLocationState.Released:
                        //Don't care about released state in this demo
                        int[] selectedIndexes = new int[WordGame.selectedSquares.Count];
                        int index = 0;
                        foreach (GameSquare gs in WordGame.selectedSquares)
                            selectedIndexes[index++] = gs.index+1;

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
                                if (gs.collisionRect.Contains((int) fingerX, (int) fingerY))
                                {
                                    if (WordGame.selectedSquares.Count > 0 && !WordGame.selectedSquares.Contains(gs) &&
                                        WordGame.IsValidNextSquare(WordGame.selectedSquares.Last(), gs))
                                    {
                                        gs.color = Color.Red;
                                        WordGame.selectedSquares.Add(gs);
                                    }
                                    else
                                    {
                                        if (WordGame.selectedSquares.Count - 2 >= 0 &&
                                            WordGame.selectedSquares[WordGame.selectedSquares.Count - 2] == gs)
                                        {
                                            WordGame.selectedSquares.Last().color = Color.Blue;
                                            WordGame.selectedSquares.RemoveAt(WordGame.selectedSquares.Count - 1);
                                        }
                                    }
                                }
                            }
                        }

                        break;
                }
            }

            //Animator.Update();
            //if (time <= 1.5 )
            //{
            //    time += 0.05;
            //    foreach (GameSquare s in WordGame.squares)
            //        if (1.0 != s.scale)
            //            s.scale = Easings.easeOutBounce(time, s.tempScale, 1.0f, 3.5);
            //}
            //if (time <= 1.5)
            //{
            //    time += 0.05;
            //    int i = 0;

            //    if (time >= 0)
            //        foreach (GameSquare s in WordGame.squares)
            //        {
            //            if(i < time * 20)
            //                s.scale = MathFunctions.easeOutBounce(time, 0.0f, 1.0f, 1.5);
            //            i++;
            //        }
            //        //for (int i = 0; i < WordGame.squares.Length && i < time * 20; i++)
            //            //WordGame.squares[i].scale = MathFunctions.easeOutBounce(time, 0.0f, 1.0f, 2);
            //}

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

            if(bombing)
                spriteBatch.Draw(textureRed, new Rectangle(0, 0, 480, 800), Color.Red);

            foreach (GameSquare gs in WordGame.squares)
            {
                if (gs != null)
                {
                    if (gs.color == Color.Blue)
                        //spriteBatch.Draw(textureBlue, gs.rect, Color.Blue);
                        //spriteBatch.Draw(textureBlue, new Vector2(gs.rect.X, gs.rect.Y), gs.rect, Color.Blue, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), gs.scale, SpriteEffects.None, 0.0f);
                        spriteBatch.Draw(textureBlue, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.FromNonPremultiplied(63,169,245,255), 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), gs.Scale, SpriteEffects.None, 0.0f);
                    //                    spriteBatch.Draw(textureBlue, gs.rect, Color.Blue);

                    if (gs.color == Color.Red || (bombed_indices != null && bombed_indices.Contains(gs.GetServerIndex()) && bombing ))
                        spriteBatch.Draw(textureRed, gs.rect, Color.Red); 
                    //                    spriteBatch.Draw(textureRed, gs.rect, Color.Red);

                    spriteBatch.DrawString(letterValueFont, gs.GetValue().ToString().ToLower(), new Vector2(gs.rect.X + 89, gs.rect.Y + 100), Color.DarkGray, 0.0f, letterFont.MeasureString(gs.GetValue().ToString().ToLower()) / 2, 1.0f, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(letterFont, gs.letter.ToString().ToUpper(), new Vector2(gs.rect.X + 46, gs.rect.Y + 48), Color.White, 0.0f, letterFont.MeasureString(gs.letter.ToLower())/2, 1.0f, SpriteEffects.None, 0.0f);
                }

            }

//            spriteBatch.DrawString(labelsFont, "Time Left? 1:34", new Vector2(80, 650), Color.Lavender);
            spriteBatch.DrawString(labelsFont, "You: " + WordGame.player1Score.ToString(), new Vector2(10, 650), Color.Green);
            spriteBatch.DrawString(labelsFont, "Him: " + WordGame.player2Score.ToString(), new Vector2(10, 700), Color.Red);
            spriteBatch.DrawString(labelsFont, WordGame.GetLetters(), new Vector2(20, 750), Color.White);

            spriteBatch.End();
        }
    }

    // username: word_crunch
    // password: word_crunch
}

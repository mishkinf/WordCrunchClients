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
using WordCruncherWP7.CrunchEventArgs;

namespace WordCruncherWP7
{
    public partial class GamePage : PhoneApplicationPage
    {
        GameTimer timer;
        ContentManager content;
        SpriteBatch spriteBatch;

        Texture2D textureBlue;
        Texture2D textureRed;
        Texture2D textureBomb;

        SpriteFont letterFont;
        SpriteFont letterValueFont;
        SpriteFont labelsFont;

        RenderTarget2D renderTarget;

        private float scale = 1.0f;
        private double time = 0;

        bool bombing = false;
        int bomb_ticks = 50;
        int[] bombed_indices = {};

        // A model for our ground
        //Model ground;
        Quad quad;
        BasicEffect quadEffect;
        VertexDeclaration vertexDeclaration;

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
            CrunchCore.OnBombed += new EventHandler<BombedArgs>(CrunchCore_OnBombed);

            TouchPanel.EnabledGestures =
                GestureType.DoubleTap | GestureType.Hold;

            quad = new Quad(Vector3.Zero, Vector3.Backward, Vector3.Up, 1, 1);
            //Animator.AddAnimation(new Animation(0.2f, 1.0f, WordGame.squares[0,0].ScaleProperty, 0.5f, Easings.easeOutBounce));
            //foreach (GameSquare s in WordGame.squares)
            //{
            //    s.scale = 0.2f;
            //}
        }


        void CrunchCore_OnBombed(object sender, BombedArgs e)
        {
            if (e.bombMessage.player_index == Constants.PlayerIndex)
            {
                bombing = true;
                bomb_ticks = 100;

                this.bombed_indices = e.bombMessage.bombs;
            }
            else
            {
                foreach(int index in e.bombMessage.bombs)
                {
                    foreach (GameSquare s in WordGame.squares)
                    {
                        if (s.index == index)
                            s.hasBomb = false;
                    }
                }
            }
            
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

            textureBomb = content.Load<Texture2D>("bomb");
            
            letterFont = content.Load<SpriteFont>("CourierNew");
            letterValueFont = content.Load<SpriteFont>("squareValue");
            labelsFont = content.Load<SpriteFont>("labelsFont");

            renderTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth, spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight);
            //ground = content.Load<Model>("Ground");

            quadEffect = new BasicEffect(spriteBatch.GraphicsDevice);
            quadEffect.EnableDefaultLighting();

            Matrix view = Matrix.CreateLookAt(new Vector3(30.0f, 6.0f, 0.0f) * 1.0f, new Vector3(0.0f, 10.0f, 0.0f), Vector3.Forward);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, spriteBatch.GraphicsDevice.Viewport.AspectRatio, 0.01f, 100f);

            quadEffect.World = Matrix.CreateScale(20.0f, 1f, 40.0f);
            quadEffect.View = view;
            quadEffect.Projection = projection;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = textureBlue;

            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                }
            );

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

            // get any gestures that are ready.
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gs = TouchPanel.ReadGesture();
                switch (gs.GestureType)
                {
                    case GestureType.DoubleTap:
                        // move the poem screen vertically by the drag delta
                        // amount.
                        addBomb(gs.Position);
                        break;
                    case GestureType.Hold:
                        addWall(gs.Position);
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

        private void addBomb(Vector2 tap)
        {
            foreach (GameSquare gs in WordGame.squares)
            {
                if (gs.collisionRect.Contains((int)tap.X, (int)tap.Y) )
                {
                    gs.hasBomb = true;
                    CrunchCore.SendMessage(new DropBombMessage(gs.index + 1));
                }
            }
        }

        private void addWall(Vector2 hold)
        {
            foreach (GameWall gw in WordGame.walls)
            {
                if (gw.collisionRect.Contains((int)hold.X, (int)hold.Y) && gw.enabled == false)
                {
                    gw.enabled = true;
                    CrunchCore.SendMessage(new DropWallMessage(gw.index1, gw.index2));
                }
            }
        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Transparent);
            
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin();

            foreach (GameSquare gs in WordGame.squares)
            {
                if (gs != null)
                {
                    if (gs.color == Color.Blue)
                        spriteBatch.Draw(textureBlue, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.White, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), gs.Scale, SpriteEffects.None, 0.0f);

                    float redScale = gs.Scale;

                    if (bombing && bombed_indices.Contains(gs.index))
                    {
                        //double dist = Math.Pow(2, WordGame.GetLetterDistance(gs.index, bombed_indices[0]));
                        double sine = (Math.Sin(bomb_ticks/3.0) + 1)/3.0;
                        float scaleOffset = (float)((bomb_ticks / 100.0) * sine);

                        redScale = gs.Scale + scaleOffset;
                    }

                    if (gs.color == Color.Red || (bombed_indices != null && bombed_indices.Contains(gs.index) && bombing))
                    {
                        spriteBatch.Draw(textureRed, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.White, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), redScale, SpriteEffects.None, 0.0f);
                        //spriteBatch.DrawString(letterValueFont, gs.GetValue().ToString().ToLower(), new Vector2(gs.rect.X + 89, gs.rect.Y + 100), Color.DarkGray, 0.0f, letterFont.MeasureString(gs.GetValue().ToString().ToLower()) / 2, redScale, SpriteEffects.None, 0.0f);
                        spriteBatch.DrawString(letterFont, gs.letter.ToString().ToUpper(), new Vector2(gs.rect.X + 46, gs.rect.Y + 48), Color.White, 0.0f, letterFont.MeasureString(gs.letter.ToLower()) / 2, redScale, SpriteEffects.None, 0.0f);
                    }
                    else
                    {
                        //spriteBatch.DrawString(letterValueFont, gs.GetValue().ToString().ToLower(), new Vector2(gs.rect.X + 89, gs.rect.Y + 100), Color.DarkGray, 0.0f, letterFont.MeasureString(gs.GetValue().ToString().ToLower()) / 2, redScale, SpriteEffects.None, 0.0f);
                        spriteBatch.DrawString(letterFont, gs.letter.ToString().ToUpper(), new Vector2(gs.rect.X + 46, gs.rect.Y + 48), Color.White, 0.0f, letterFont.MeasureString(gs.letter.ToLower()) / 2, redScale, SpriteEffects.None, 0.0f);
                    }
                       
                    if(gs.hasBomb)
                        spriteBatch.Draw(textureBomb, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.White, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), redScale, SpriteEffects.None, 0.0f);
                }

            }
            
//            spriteBatch.DrawString(labelsFont, "Time Left? 1:34", new Vector2(80, 650), Color.Lavender);
            spriteBatch.DrawString(labelsFont, "You: " + WordGame.scoreYou.ToString(), new Vector2(10, 650), Color.Green);
            spriteBatch.DrawString(labelsFont, "Him: " + WordGame.scoreOpponent.ToString(), new Vector2(10, 700), Color.Red);
            spriteBatch.DrawString(labelsFont, WordGame.GetLetters(), new Vector2(20, 750), Color.White);

            spriteBatch.End();

            spriteBatch.GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.FrontToBack , BlendState.NonPremultiplied);

            //spriteBatch.Draw((Texture2D)renderTarget, new Vector2(0, 450.0f), new Rectangle(0, 0, 480, 480), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipVertically, 0.0f);
            //spriteBatch.Draw((Texture2D)renderTarget, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            //Matrix view = Matrix.CreateLookAt(new Vector3(30.0f, 6.0f, 0.0f) * 1.0f, new Vector3(0.0f, 10.0f, 0.0f), Vector3.Forward);
            //Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, spriteBatch.GraphicsDevice.Viewport.AspectRatio, 0.01f, 100f);

            // Set our sampler state to allow the ground to have a repeated texture
            //spriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // Draw the ground scaled to our world
            //ground.Draw(Matrix.CreateScale(20.0f, 1f, 40.0f), view, projection);
                foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    spriteBatch.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
                        PrimitiveType.TriangleList,
                        quad.Vertices, 0, 4,
                        quad.Indexes, 0, 2);
                }

            spriteBatch.End();
        }
    }
}

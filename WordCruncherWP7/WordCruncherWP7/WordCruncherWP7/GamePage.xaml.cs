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
using WordCrunchWP7;

namespace WordCruncherWP7
{
    public partial class GamePage : PhoneApplicationPage
    {
        GameTimer timer;
        ContentManager content;
        SpriteBatch spriteBatch;
        GameInput input;

        Texture2D textureBlue, textureRed, textureGray, textureBomb, textureDelete, fadeUp, timerGraphic;
        SpriteFont letterFont, letterValueFont, labelsFont, timerFont, scoresFont;
        RenderTarget2D renderTarget, renderTargetFlipped;

        Bombs bombs = new Bombs();

        Quad quad;
        BasicEffect quadEffect;
        VertexDeclaration vertexDeclaration;

        SparksParticleSystem yourParticles;
        SparksParticleSystem enemyParticles;
        StopWatch stopwatch;

        public GamePage()
        {
            InitializeComponent();

            // Get the application's ContentManager
            content = (Application.Current as App).Content;

            input = new GameInput();
            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;
 
            CrunchCore.OnBombed += new EventHandler<BombedArgs>(CrunchCore_OnBombed);
            CrunchCore.OnGoodGuess += new EventHandler<GoodGuessArgs>(CrunchCore_OnGoodGuess);
            TouchPanel.EnabledGestures = GestureType.DoubleTap | GestureType.Hold;

            stopwatch = new StopWatch();
            stopwatch.Start();
            quad = new Quad(Vector3.Zero, Vector3.Up, Vector3.Backward, 10, 9);
        }

        void CrunchCore_OnGoodGuess(object sender, GoodGuessArgs e)
        {
            WordScored(e.Message.Player, e.Message.Word);
        }

        void CrunchCore_OnBombed(object sender, BombedArgs e)
        {

            bombs.DropBomb(e.Message.bombs);
            //e.Message.Word.SetBombed();
            WordScored(e.Message.Player, e.Message.Word);
        }

        private void WordScored(ePlayer player, Word word)
        {
            switch (player)
            {
                case ePlayer.You:
                    CrunchCore.yourWords.Add(word);

                    foreach(GameSquare s in word.GameSquares)
                        yourParticles.AddParticles(new Vector2(s.rect.X + 50, s.rect.Y + 50));

                    break;
                case ePlayer.Opponent:
                    CrunchCore.opponentsWords.Add(word);
                    
                    foreach (GameSquare s in word.GameSquares)
                        enemyParticles.AddParticles(new Vector2(s.rect.X + 50, s.rect.Y + 50));
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);
            yourParticles = new SparksParticleSystem(content, spriteBatch, 30, "spark");
            enemyParticles = new SparksParticleSystem(content, spriteBatch, 30, "spark_black");

            yourParticles.LoadContent();
            enemyParticles.LoadContent();

            textureBlue = content.Load<Texture2D>("square_blue2");
            textureRed = content.Load<Texture2D>("square_red");
            textureGray = content.Load<Texture2D>("square_gray");
            timerGraphic = content.Load<Texture2D>("timergraphic");

            fadeUp = content.Load<Texture2D>("fadeup3");
            textureBomb = content.Load<Texture2D>("bomb");
            textureDelete = content.Load<Texture2D>("deletemetexture");
            letterFont = content.Load<SpriteFont>("CourierNew");
            letterValueFont = content.Load<SpriteFont>("squareValue");
            labelsFont = content.Load<SpriteFont>("usernamesFont");
            timerFont = content.Load<SpriteFont>("timerFont");
            scoresFont = content.Load<SpriteFont>("labelsFont");

            renderTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth, spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight);
            renderTargetFlipped = new RenderTarget2D(spriteBatch.GraphicsDevice, spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth, spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth);

            quadEffect = new BasicEffect(spriteBatch.GraphicsDevice);

            timer.Start();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timer.Stop();

            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            bombs.UpdateBomb();

            input.Update(TouchPanel.GetState());

            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gs = TouchPanel.ReadGesture();
                switch (gs.GestureType)
                {
                    case GestureType.DoubleTap:
                        bombs.AddBomb(gs.Position);
                        break;
                }
            }

            UpdateSprite(e);
        }

        void UpdateSprite(GameTimerEventArgs e)
        {
            yourParticles.Update(e.ElapsedTime);
            enemyParticles.Update(e.ElapsedTime);

            Matrix view = Matrix.CreateLookAt(new Vector3(0, 10f, 10f), new Vector3(0.0f, 7.0f, 0.0f), Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, spriteBatch.GraphicsDevice.Viewport.AspectRatio, 0.01f, 1000f);

            quadEffect.World = Matrix.CreateScale(2.0f, 1f, 1f);
            quadEffect.View = view;
            quadEffect.Projection = projection;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = renderTargetFlipped;
            quadEffect.EnableDefaultLighting();

            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                }
            );
        }   

        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Transparent);
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin();
            float scaleChange = 1f;

            foreach (GameSquare gs in WordGame.squares)
            {
                scaleChange = bombs.IsBombing(gs.index) ? gs.Scale - bombs.GetScaleOffset() : 1f;

                if (gs.color == Color.Blue || bombs.IsBombing(gs.index))
                {
                    spriteBatch.Draw(textureBlue, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.White, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2),scaleChange, SpriteEffects.None, 0.0f);
                } else if (gs.color == Color.Gray)
                {
                    spriteBatch.Draw(textureGray, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.White, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), gs.Scale, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(letterFont, gs.letter.ToString().ToUpper(), new Vector2(gs.rect.X + 46, gs.rect.Y + 48), Color.FromNonPremultiplied(51, 160, 232, 255), 0.0f, letterFont.MeasureString(gs.letter.ToLower()) / 2, gs.Scale, SpriteEffects.None, 0.0f);
                }
                   
                if (gs.hasBomb)
                    spriteBatch.Draw(textureBomb, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.White, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), gs.Scale, SpriteEffects.None, 0.0f);
            }

            foreach (GameSquare gs in WordGame.squares)
            {
                scaleChange = bombs.IsBombing(gs.index) ? gs.Scale + bombs.GetScaleOffset() : 1f;

                if (gs.color == Color.Gray || (!gs.yourBomb && bombs.IsBombing(gs.index)))
                {
                    spriteBatch.Draw(textureGray, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.White, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), scaleChange, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(letterFont, gs.letter.ToString().ToUpper(), new Vector2(gs.rect.X + 46, gs.rect.Y + 48), Color.FromNonPremultiplied(51, 160, 232, 255), 0.0f, letterFont.MeasureString(gs.letter.ToLower()) / 2, scaleChange, SpriteEffects.None, 0.0f);
                }
                else
                {
                    spriteBatch.DrawString(letterFont, gs.letter.ToString().ToUpper(), new Vector2(gs.rect.X + 46, gs.rect.Y + 48), Color.White, 0.0f, letterFont.MeasureString(gs.letter.ToLower()) / 2, scaleChange, SpriteEffects.None, 0.0f);
                }
                       
                if(gs.hasBomb)
                    spriteBatch.Draw(textureBomb, new Vector2(gs.rect.X + gs.rect.Width / 2, gs.rect.Y + gs.rect.Height / 2), null, Color.White, 0.0f, new Vector2(gs.rect.Width / 2, gs.rect.Height / 2), scaleChange, SpriteEffects.None, 0.0f);

                yourParticles.Draw(e.ElapsedTime);
                enemyParticles.Draw(e.ElapsedTime);
            }

            spriteBatch.End();

            spriteBatch.GraphicsDevice.SetRenderTarget(renderTargetFlipped);
            spriteBatch.Begin(SpriteSortMode.FrontToBack , BlendState.NonPremultiplied);
            spriteBatch.Draw((Texture2D)renderTarget, Vector2.Zero, new Rectangle(0, 0, 480, 480), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0.0f);
            spriteBatch.End();

            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            spriteBatch.Draw((Texture2D)renderTarget, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();

            spriteBatch.Begin();
            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                spriteBatch.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    quad.Vertices, 0, 4,
                    quad.Indexes, 0, 2);
            }
            spriteBatch.Draw(fadeUp, new Vector2(0, 480), Color.White);
            float textx1 = 110, textx2 = 320;
            spriteBatch.DrawString(scoresFont, WordGame.scoreYou.ToString(), new Vector2(textx1, 660), Color.White);
            spriteBatch.DrawString(scoresFont, WordGame.scoreOpponent.ToString(), new Vector2(textx2, 660), Color.White);

            spriteBatch.DrawString(labelsFont, Globals.YourUsername, new Vector2(textx1, 750), Color.LightGray);
            spriteBatch.DrawString(labelsFont, Globals.OpponentUsername, new Vector2(textx2, 750), Color.LightGray);
            spriteBatch.Draw(timerGraphic, new Vector2(45, 670), Color.White);
            
            if(stopwatch.GetElapsedTimeSecs() > 10 || (stopwatch.GetElapsedTimeSecs() <= 10 && stopwatch.GetTimeSpanInterval().Milliseconds % 800 <= 400))
                spriteBatch.DrawString(timerFont, stopwatch.GetFormattedTimeRemaining(), new Vector2(219, 717), Color.FromNonPremultiplied(7, 197, 236, 255));

            spriteBatch.End();

        }
    }


}

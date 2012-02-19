using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using WordCruncherWP7;

namespace WordCrunchWP7
{
    public class FireworksParticleSystem : ParticleSystem
    {
        public FireworksParticleSystem(ContentManager content, SpriteBatch spriteBatch, int howManyEffects, String texture)
            : base(content, spriteBatch, howManyEffects)
        {
            this.textureFilename = texture;

            base.Initialize();
        }

        protected override void InitializeConstants()
        {
            minNumParticles = 8;
            maxNumParticles = 15;

            textureFilename = "spark";

            minInitialSpeed = 250.0f;
            maxInitialSpeed = 300.0f;

            minAcceleration = 200.0f;
            maxAcceleration = 250.0f;

            minRotationSpeed = 0.0f;
            maxRotationSpeed = 3.0f;

            minLifetime = 0.01f;
            maxLifetime = 0.8f;

            minScale = 0.1f;
            maxScale = 0.5f;
        }
    }
}

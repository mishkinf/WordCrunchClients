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
    public class SmokeTrailParticleSystem : ParticleSystem
    {
        public SmokeTrailParticleSystem(ContentManager content, SpriteBatch spriteBatch, int howManyEffects, String texture)
            : base(content, spriteBatch, howManyEffects)
        {
            this.textureFilename = texture;
            base.Initialize();
        }

        protected override void InitializeConstants()
        {
            minNumParticles = 5;
            maxNumParticles = 15;

            minInitialSpeed = 10.0f;
            maxInitialSpeed = 30.0f;

            minAcceleration = 1.0f;
            maxAcceleration = 10.0f;

            minRotationSpeed = 0.0f;
            maxRotationSpeed = 3.0f;

            minLifetime = 0.2f;
            maxLifetime = 1.5f;

            minScale = 0.05f;
            maxScale = 0.7f;
        }
    }
}

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
    public class SparksParticleSystem : ParticleSystem
    {
        public SparksParticleSystem(ContentManager content, SpriteBatch spriteBatch, int howManyEffects, String texture)
            : base(content, spriteBatch, howManyEffects)
        {
            textureFilename = texture;
            base.Initialize();
        }

        protected override void InitializeConstants()
        {
            minNumParticles = 10;
            maxNumParticles = 40;

            minInitialSpeed = 50.0f;
            maxInitialSpeed = 80.0f;

            minAcceleration = 20.0f;
            maxAcceleration = 80.0f;

            minRotationSpeed = 0.0f;
            maxRotationSpeed = 10.0f;

            minLifetime = 0.2f;
            maxLifetime = 3.0f;

            minScale = 0.05f;
            maxScale = 0.3f;
        }
    }
}

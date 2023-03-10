using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    /// <summary>
    /// Controls playback of a CelAnimationSequence.
    /// </summary>
    public class CelAnimationPlayer
    {
        private CelAnimationSequence celAnimationSequence;
        private int celIndex;
        private float celTimeElapsed;
        private Rectangle celSourceRectangle;

        /// <summary>
        /// Begins or continues playback of a CelAnimationSequence.
        /// </summary>
        public void Play(CelAnimationSequence celAnimationSequence)
        {
            if (celAnimationSequence == null)
            {
                throw new Exception("CelAnimationPlayer.PlayAnimation received null CelAnimationSequence");
            }
            // If this animation is already running, do not restart it...
            if (celAnimationSequence != this.celAnimationSequence)
            {
                this.celAnimationSequence = celAnimationSequence;
                celIndex = 0;
                celTimeElapsed = 0.0f;

                celSourceRectangle.X = 0;
                celSourceRectangle.Y = 0;
                celSourceRectangle.Width = this.celAnimationSequence.CelWidth;
                celSourceRectangle.Height = this.celAnimationSequence.CelHeight;
            }
        }

        /// <summary>
        /// Update the state of the CelAnimationPlayer.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, int row)
        {
            celSourceRectangle.Y = celSourceRectangle.Height * row;
            if (celAnimationSequence != null)
            {
                celTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (celTimeElapsed >= celAnimationSequence.CelTime)
                {
                    celTimeElapsed -= celAnimationSequence.CelTime;

                    // Advance the frame index looping as appropriate...
                    celIndex = (celIndex + 1) % celAnimationSequence.CelCount;

                    celSourceRectangle.X = celIndex * celSourceRectangle.Width;

                    //if (celIndex == 0)
                    //{
                    //    celSourceRectangle.Y += celSourceRectangle.Height;
                    //    if (celSourceRectangle.Y >= celAnimationSequence.Texture.Height)
                    //    {
                    //        celSourceRectangle.Y = 0;
                    //    }
                    //}
                }
            }
        }

        /// <summary>
        /// Draws the current cel of the animation.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, SpriteEffects spriteEffects)
        {
            if (celAnimationSequence != null)
            {
                spriteBatch.Draw(celAnimationSequence.Texture, position, celSourceRectangle, Color.White, 0.0f, Vector2.Zero, scale, spriteEffects, 0.0f);
            }
        }
    }
}

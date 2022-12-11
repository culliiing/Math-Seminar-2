using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Seminar_2
{
    internal class Ball
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity; // angle determined by mouse position?
        float scale;
        bool fired;

        public Ball(Texture2D texture, Vector2 velocity, float radius)
        {
            this.texture = texture;
            this.position = new Vector2(0, Game1.windowSize.Y-this.texture.Height);
            this.velocity = velocity;
            this.scale = radius / (texture.Width / 2);
            this.fired = false;
        }

        public void Update(GameTime gameTime)
        {
            if (fired)
                position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}

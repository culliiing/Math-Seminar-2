using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Seminar_2
{
    internal class Car // or class for path on which travelling objects can be made
    {
        Texture2D texture;
        public Texture2D Texture { get { return texture; } }

        Vector2 position;
        float radius;
        float speed;

        Vector2 circleCenter = new Vector2(600, 400);
        float angle;

        Rectangle hitbox;
        public Rectangle Hitbox { get { return hitbox; } }

        public Car(Texture2D texture, float speed, float radius)
        {
            this.texture = texture;
            this.radius = radius;
            this.speed = speed;

            this.hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Car(Texture2D texture)
        {
            this.texture = texture;
            this.radius = 100f;
            this.speed = 1f;
            this.hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            position = circleCenter + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;
            angle += MathHelper.ToRadians(1f * speed);

            // OBS! Hitbox does not rotate
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, angle + MathHelper.Pi/2, new Vector2(Texture.Width / 2, Texture.Height / 2), 1f, SpriteEffects.None, 0f);
            //DrawHitbox(spriteBatch);
        }

        public void DrawHitbox(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.Green * 0.5f);
        }

    }
}

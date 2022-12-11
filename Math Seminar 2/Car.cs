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
        Vector2 orbitingOrigin;
        float rotation;
        float radius;

        Rectangle hitbox;

        public Rectangle Hitbox { get { return hitbox; } }

        public Car(Texture2D texture, float rotation, float radius)
        {
            this.texture = texture;
            this.rotation = rotation;
            this.radius = radius;

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Car(Texture2D texture)
        {
            this.texture = texture;
            position = new Vector2(600, 400);
            radius = 100;
            orbitingOrigin = new Vector2(radius, radius);

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            hitbox.X = (int)position.X; // Only the rotation of the car changes, not the position, so these lines of code does nothing.
            hitbox.Y = (int)position.Y; // Look up TRANSFORMATION MATRIX for rotating the hitbox alongside the sprite: https://en.wikipedia.org/wiki/Transformation_matrix

            rotation +=0.01f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, orbitingOrigin, 1f, SpriteEffects.None, 0);
            DrawHitbox(spriteBatch);
        }

        public void DrawHitbox(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.Green * 0.5f);
        }

    }
}

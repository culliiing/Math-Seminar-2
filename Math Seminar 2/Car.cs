using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Vector2 origin;
        Vector2 orbitingOrigin;
        float rotation;
        float radius;
        float theta = 0;
        Rectangle hitbox;

        public readonly Color[] TextureData;


        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, texture.Width, texture.Height);
            }
        }
        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-origin, 0)) *
                  Matrix.CreateRotationZ(rotation) *
                  Matrix.CreateTranslation(new Vector3(position, 0));
            }
        }
        
        public Rectangle Hitbox { get { return hitbox; } }

        public Car(Texture2D texture, float rotation, float radius)
        {
            this.texture = texture;
            this.rotation = rotation;
            this.radius = radius;

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            TextureData = new Color[texture.Width * texture.Height];
            texture.GetData(TextureData);
        }

        public Car(Texture2D texture)
        {
            this.texture = texture;
            position = new Vector2(600, 400);
            origin = position;
            radius = 100;
            orbitingOrigin = new Vector2(radius, radius);

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            //hitbox.X = (int)position.X; // Only the rotation of the car changes, not the position, so these lines of code does nothing.
            //hitbox.Y = (int)position.Y; // Look up TRANSFORMATION MATRIX for rotating the hitbox alongside the sprite: https://en.wikipedia.org/wiki/Transformation_matrix

            // Create a rotation matrix
            // Get the corner points of the rotated object
            //Vector2 topLeft = new Vector2(position.X - startPos.X, position.Y - startPos.Y);
            //Vector2 topRight = new Vector2(position.X + startPos.X, position.Y - startPos.Y);
            //Vector2 bottomLeft = new Vector2(position.X - startPos.X, position.Y + startPos.Y);
            //Vector2 bottomRight = new Vector2(position.X + startPos.X, position.Y + startPos.Y);

            // Rotate the corner points by the object's rotation
            //topLeft = Vector2.Transform(topLeft, Matrix.CreateRotationZ(rotation));
            //topRight = Vector2.Transform(topRight, Matrix.CreateRotationZ(rotation));
            //bottomLeft = Vector2.Transform(bottomLeft, Matrix.CreateRotationZ(rotation));
            //bottomRight = Vector2.Transform(bottomRight, Matrix.CreateRotationZ(rotation));

            //// Check if any of the corner points are contained within the bounding box of the other object
            
            //hitbox.X = (int)topLeft.X;
            //hitbox.Y = (int)topLeft.Y;
           

            rotation += 0.01f;
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

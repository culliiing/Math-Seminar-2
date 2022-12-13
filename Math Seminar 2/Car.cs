using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math_Seminar_2
{
    internal class Car : Sprite // or class for path on which travelling objects can be made
    {
        public Texture2D Texture { get { return texture; } }


        Vector2 orbitingOrigin;
        float radius;
        float theta = 0;
        Rectangle hitbox;

        float speed = 3;

        float carPos;

        float rotation = 0;

        bool movingForward = true;

        Texture2D carTex;

        SimplePath path;

        public Car(Texture2D carTex, SimplePath path)
        {
            this.path = path;
            this.carTex = carTex;

            origin = new Vector2(carTex.Width / 2, carTex.Height / 2);

            SetPath();
        }

        public void Update()
        {
            if(carPos > path.endT && movingForward)
            {
                movingForward = false;
            }
        }

        public Rectangle Hitbox { get { return hitbox; } }

        public Car(Texture2D texture, float rotation, float radius) : base(texture)
        {
            this.texture = texture;
            this.rotation = rotation;
            this.radius = radius;

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            TextureData = new Color[texture.Width * texture.Height];
            texture.GetData(TextureData);
        }

        public Car(Texture2D texture) : base(texture)
        {
            this.texture = texture;
            position = new Vector2(600, 400);
            origin = position;
            radius = 100;
            orbitingOrigin = new Vector2(radius, radius);

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        private void SetPath()
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


            path.AddPoint(new Vector2(0, 100));
            path.AddPoint(new Vector2(180, 400));
            path.AddPoint(new Vector2(360, 100));
            path.AddPoint(new Vector2(540, 400));
            path.AddPoint(new Vector2(720, 100));
            path.AddPoint(new Vector2(900, 400));
            path.AddPoint(new Vector2(1080, 100));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawPath(spriteBatch);

            spriteBatch.Draw(carTex, path.GetPos(carPos), null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }

        public void DrawPath(SpriteBatch spriteBatch)
        {
            path.Draw(spriteBatch);
            path.DrawPoints(spriteBatch);
        }


    }
}

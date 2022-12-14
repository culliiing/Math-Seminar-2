using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spline;
using System;

namespace Math_Seminar_2
{
    internal class Car // or class for path on which travelling objects can be made
    {
        Vector2 velocity; // vinkelhastiget?

        Vector2 origin;

        Vector2 directionVector;

        float speed = 3;

        float carPos;

        float angle;

        Vector2 lastPos, currentPos;

        float rotation = 1.1f;

        bool movingForward = true;

        Texture2D texture;
        public Texture2D Texture { get { return texture; } }

        SimplePath path;

        Rectangle hitbox;
        public Rectangle Hitbox { get { return hitbox; } }

        public Car(Texture2D texture, SimplePath path)
        {
            this.path = path;
            this.texture = texture;

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            hitbox = new Rectangle((int)currentPos.X, (int)currentPos.Y, texture.Width, texture.Height);

            SetPath();
        }

        public void Update()
        {
            hitbox.X = (int)currentPos.X;
            hitbox.Y = (int)currentPos.Y;

            lastPos = path.GetPos(carPos); //takes last position to calculate direction

            if (carPos > path.endT && movingForward)
            {
                movingForward = false;
            }
            else if (carPos < path.beginT && !movingForward)
            {
                movingForward = true;
            }

            if (movingForward)
            {
                carPos += speed;
            }
            else
            {
                carPos -= speed;
            }

            currentPos = path.GetPos(carPos); //takes current posistion to calculate 

            if (movingForward)
            {
                directionVector = new Vector2(currentPos.X - lastPos.X, currentPos.Y - lastPos.Y);
            }
            if (!movingForward)
            {
                directionVector = new Vector2(lastPos.X - currentPos.X, lastPos.Y - currentPos.Y);
            }

            angle = (float)Math.Atan2(directionVector.Y, directionVector.X);

            rotation = angle;

        }

        private void SetPath()
        {
            path.Clean();
            carPos = path.beginT;

            path.AddPoint(new Vector2(0, 100));
            path.AddPoint(new Vector2(180, 400));
            path.AddPoint(new Vector2(400, 100));
            path.AddPoint(new Vector2(540, 250));
            path.AddPoint(new Vector2(720, 100));
            path.AddPoint(new Vector2(800, 600));
            path.AddPoint(new Vector2(1080, 100));

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //DrawPath(spriteBatch);

            spriteBatch.Draw(texture, path.GetPos(carPos), null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }

        //public void DrawPath(SpriteBatch spriteBatch)
        //{
        //    path.Draw(spriteBatch);
        //    path.DrawPoints(spriteBatch);
        //}
    }
}

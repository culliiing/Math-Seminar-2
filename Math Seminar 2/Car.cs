using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spline;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using SharpDX.Direct3D9;
using System.Diagnostics;

namespace Math_Seminar_2
{
    internal class Car // or class for path on which travelling objects can be made
    {

        //float maxAngle = 90f;

        Vector2 velocity; // vinkelhastiget?

        Vector2 origin;

        Vector2 distanceVector;

        float speed = 3;

        float carPos;

        float angle;

        Vector2 lastPos, currentPos;

        float rotation = 1.1f;

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
                distanceVector = new Vector2(currentPos.X - lastPos.X, currentPos.Y - lastPos.Y);
            }
            if (!movingForward)
            {
                distanceVector = new Vector2(lastPos.X - currentPos.X, lastPos.Y - currentPos.Y);
            }

            angle = (float)Math.Atan2(distanceVector.Y, distanceVector.X);

            angle = MathHelper.ToDegrees(angle);

            //if (angle >= maxAngle)
            //{
            //    angle = maxAngle;
            //}

            //if (angle <= -maxAngle)
            //{
            //    angle = -maxAngle;
            //}
            //Debug.WriteLine(distanceVector.ToString());
            //Debug.WriteLine(angle.ToString());

            rotation = MathHelper.ToRadians(angle);

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

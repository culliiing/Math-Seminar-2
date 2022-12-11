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

namespace Math_Seminar_2
{
    internal class Car // or class for path on which travelling objects can be made
    {
        Vector2 velocity; // vinkelhastiget?

        Vector2 origin;

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
            else if(carPos < path.beginT && !movingForward)
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
        }

        private void SetPath()
        {
            path.Clean();
            carPos = path.beginT;

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

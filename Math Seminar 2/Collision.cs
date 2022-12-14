using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Math_Seminar_2
{
    internal class Collision
    {
        static Color[] dataA;
        static Color[] dataB;

        //Hitbox does not rotate, research on it and Transformation Matrises here: https://en.wikipedia.org/wiki/Transformation_matrix 

        public static bool PixelCollision(Car car, Ball ball)
        {
            dataA = new Color[car.Texture.Width * car.Texture.Height];
            car.Texture.GetData(dataA);
            dataB = new Color[ball.Texture.Width * ball.Texture.Height];
            ball.Texture.GetData(dataB);

            int top = Math.Max(car.Hitbox.Top, ball.Hitbox.Top);
            int bottom = Math.Min(car.Hitbox.Bottom, ball.Hitbox.Bottom);
            int left = Math.Max(car.Hitbox.Left, ball.Hitbox.Left);
            int right = Math.Min(car.Hitbox.Right, ball.Hitbox.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = dataA[(x - car.Hitbox.Left) + (y - car.Hitbox.Top) * car.Hitbox.Width];
                    Color colorB = dataB[(x - ball.Hitbox.Left) + (y - ball.Hitbox.Top) * ball.Hitbox.Width];

                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void HandleCollision(Car car, Ball ball)
        {
            if (PixelCollision(car, ball) && Game1.paused == false)
            {
                Debug.WriteLine("collision!");
                Game1.paused = true;
            }
        }

        public static bool Intersect(Ball ball)
        {
            Color[] pixels = new Color[ball.Texture.Width * ball.Texture.Height];
            Color[] pixels2 = new Color[ball.Texture.Width * ball.Texture.Height];
            ball.Texture.GetData<Color>(pixels2);
            try
            {
                Game1.RenderTarget.GetData(0, ball.Hitbox, pixels, 0, pixels.Length);
                for (int i = 0; i < pixels.Length; ++i)
                {
                    if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                        return true;
                }
            }
            catch
            {
                Game1.paused = true;
            }

            return false;
        }

    }
}


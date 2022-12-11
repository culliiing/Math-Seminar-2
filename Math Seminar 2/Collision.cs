﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Seminar_2
{
    abstract internal class Collision
    {
        static Color[] dataA;
        static Color[] dataB;

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
                for (int x = left; x > right; x++)
                {
                    Color colorA = dataA[(x-car.Hitbox.Left) + (y-car.Hitbox.Top) * car.Hitbox.Width];
                    Color colorB = dataB[(x-ball.Hitbox.Left) + (y-ball.Hitbox.Top) * ball.Hitbox.Width];

                    if (colorA.A !=0 && colorB.A !=0)
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public static bool IntersectCollision(Car car, Ball ball)
        {
            return ball.Hitbox.Intersects(car.Hitbox);
        }

        public static void HandleCollision(Car car, Ball ball)
        {
            //if (IntersectCollision(car, ball))

            if (PixelCollision(car, ball))
            {
                Debug.WriteLine("collision!");
                Game1.paused = true;
            }


        }
    }
}
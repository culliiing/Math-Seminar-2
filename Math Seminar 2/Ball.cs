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
    internal class Ball
    {
        MouseState mouseState;
        MouseState previousMouseState;

        Texture2D texture;
        Vector2 position;
        Vector2 direction; // angle determined by mouse position?
        float speed;
        float scale;
        bool fired;

        public Ball(Texture2D texture, float speed, float radius)
        {
            this.texture = texture;
            this.position = new Vector2(0, Game1.windowSize.Y-this.texture.Height);
            this.speed = speed;
            this.scale = radius / (texture.Width / 2);
            this.fired = false;
        }
        
        // Make a ball with default variable values
        public Ball(Texture2D texture)
        {
            this.texture = texture;
            this.position = new Vector2(0, Game1.windowSize.Y-this.texture.Height);
            this.speed = 400f;
            this.scale = 1;
            this.fired = false;
        }

        public void Update(GameTime gameTime)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (fired)
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                direction = Vector2.Normalize(mouseState.Position.ToVector2() - position);
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    fired = !fired;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position + new Vector2(texture.Width / 2, texture.Height / 2), null, Color.White, 0, new Vector2(texture.Width/2, texture.Height/2), scale, SpriteEffects.None, 0);
            DrawHitbox(spriteBatch);
        }

        public void DrawHitbox(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.Green * 0.5f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        private void UpdateHitbox()
        {
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }
    }
}

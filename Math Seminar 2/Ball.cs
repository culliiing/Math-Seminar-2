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

        public Texture2D Texture { get { return texture; } }

        Rectangle hitbox;
        public Rectangle Hitbox { get { return hitbox; } }
        public Vector2 Position { get { return new Vector2(hitbox.X, hitbox.Y); } }

        Vector2 position;
        Vector2 direction; // angle determined by mouse position?
        float speed;
        float scale;
        public float Scale { get { return scale; } }
        bool fired;

        public Ball(Texture2D texture, float speed, float radius)
        {
            this.texture = texture;
            this.scale = radius / (texture.Width / 2);
            this.position = new Vector2(0 + texture.Width*scale/2, Game1.windowSize.Y-texture.Height*scale/2);
            this.speed = speed;
            this.fired = false;

            this.hitbox = new Rectangle((int)position.X - texture.Width * (int)scale / 2, (int)position.Y - texture.Height * (int)scale / 2, texture.Width * (int)scale, texture.Height * (int)scale);
        }
        
        // Make a ball with default variable values
        public Ball(Texture2D texture)
        {
            this.texture = texture;
            this.position = new Vector2(0 + texture.Width, Game1.windowSize.Y-this.texture.Height);
            this.speed = 400f;
            this.scale = 1;
            this.fired = false;
            this.hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            UpdateHitbox();

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
            //spriteBatch.Draw(texture, position/*Position + new Vector2(texture.Width*scale / 2, texture.Height*scale / 2)*/, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0);
            DrawHitbox(spriteBatch);
        }

        public void DrawHitbox(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void UpdateHitbox()
        {
            hitbox.X = (int)position.X - texture.Width * (int)scale/2;
            hitbox.Y = (int)position.Y - texture.Height * (int)scale/2;
        }
    }
}

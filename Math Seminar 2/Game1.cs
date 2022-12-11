using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.IO;

namespace Math_Seminar_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static Point windowSize;

        Texture2D ballTexture;
        Texture2D carTexture;

        Ball ball;
        Car car;
        SimplePath path;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            windowSize = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("ball");
            carTexture = Content.Load<Texture2D>("Cloud");

            ball = new Ball(ballTexture);
            path = new SimplePath(graphics.GraphicsDevice);
            car = new Car(carTexture, path);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ball.Update(gameTime);
            car.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            ball.Draw(spriteBatch);
            car.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
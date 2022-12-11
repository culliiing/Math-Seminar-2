using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Math_Seminar_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static Point windowSize;

        Texture2D ballTexture;
        Texture2D carTexture;
        Texture2D pixel;

        Ball ball;

        Car car;
        public static bool paused = false;

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
            pixel = Content.Load<Texture2D>("1x1 pixel");

            ball = new Ball(ballTexture);
            car = new Car(carTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (paused)
            {

            }
            else
            {
                ball.Update(gameTime);
                car.Update(gameTime);
                Collision.HandleCollision(car, ball);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            ball.Draw(spriteBatch);
            //spriteBatch.Draw(pixel, new Rectangle(600, 400, 10, 10), Color.Black);
            car.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
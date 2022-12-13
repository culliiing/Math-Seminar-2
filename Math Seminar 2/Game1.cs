using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Math_Seminar_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        static RenderTarget2D renderTarget;
        public static RenderTarget2D RenderTarget { get { return renderTarget; } }

        public static Point windowSize;

        Texture2D ballTexture;
        Texture2D carTexture;
        Texture2D transparentBackground;
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
            windowSize = new Point(1080, 720);

            graphics.PreferredBackBufferWidth = windowSize.X;
            graphics.PreferredBackBufferHeight = windowSize.Y;
            graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(GraphicsDevice, Game1.windowSize.X, Game1.windowSize.Y);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            transparentBackground = Content.Load<Texture2D>("transparentBackground");
            carTexture = Content.Load<Texture2D>("Cloud");
            ballTexture = Content.Load<Texture2D>("ball");
            pixel = Content.Load<Texture2D>("1x1 pixel");

            car = new Car(carTexture);
            ball = new Ball(ballTexture);
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
                if (Collision.Intersect(ball))
                {
                    Game1.paused = true;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            DrawOnRenderTarget();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, renderTarget.Bounds, Color.White);
            ball.Draw(spriteBatch);
            car.Draw(spriteBatch);
            //spriteBatch.Draw(pixel, new Rectangle(600, 400, 10, 10), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawOnRenderTarget()
        {
            //Ändra så att GraphicsDevice ritar mot vårt render target
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();

            //Rita ut texturen. Den ritas nu ut till vårt render target istället
            //för på skärmen.
            car.Draw(spriteBatch);
            spriteBatch.Draw(transparentBackground, Vector2.Zero, Color.White);

            spriteBatch.End();

            //Sätt GraphicsDevice att åter igen peka på skärmen
            GraphicsDevice.SetRenderTarget(null);
        }
    }
}
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

        static RenderTarget2D renderTarget;
        public static RenderTarget2D RenderTarget { get { return renderTarget; } }
               
        Texture2D transparentBackground;
        Texture2D pixel;

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
            renderTarget = new RenderTarget2D(GraphicsDevice, Game1.windowSize.X, Game1.windowSize.Y);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("ball");
            carTexture = Content.Load<Texture2D>("Cloud");

            transparentBackground = Content.Load<Texture2D>("transparentBackground");

            ball = new Ball(ballTexture);
            path = new SimplePath(graphics.GraphicsDevice);
            car = new Car(carTexture, path);

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
                car.Update();
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
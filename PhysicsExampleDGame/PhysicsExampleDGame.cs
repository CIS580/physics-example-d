using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;

namespace PhysicsExampleDGame
{
    public class PhysicsExampleDGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private World world;
        private List<BallSprite> balls;


        public PhysicsExampleDGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = Constants.GAME_WIDTH;
            graphics.PreferredBackBufferHeight = Constants.GAME_HEIGHT;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            world = new World();
            world.Gravity = Vector2.Zero;

            var edges = new Body[] {
                world.CreateEdge(Vector2.Zero, new Vector2(0, Constants.GAME_HEIGHT)),
                world.CreateEdge(Vector2.Zero, new Vector2(Constants.GAME_WIDTH, 0)),
                world.CreateEdge(new Vector2(0, Constants.GAME_HEIGHT), new Vector2(Constants.GAME_WIDTH, Constants.GAME_HEIGHT)),
                world.CreateEdge(new Vector2(Constants.GAME_WIDTH, 0), new Vector2(Constants.GAME_WIDTH, Constants.GAME_HEIGHT))
            };
            foreach (var edge in edges)
            {
                edge.BodyType = BodyType.Static;
                edge.SetRestitution(1.0f);
            }
            System.Random random = new System.Random();
            balls = new List<BallSprite>();
            for (int i = 0; i < 20; i++)
            {
                var radius = random.Next(5, 50);
                var body = world.CreateCircle(
                    radius,
                    1f,
                     new Vector2(random.Next(50, 680), random.Next(50, 310)),
                     BodyType.Dynamic);
                body.LinearVelocity = new Vector2(50 - (float)random.NextDouble() * 100, 50 - (float)random.NextDouble() * 100);
                body.AngularVelocity = (float)random.NextDouble() * MathHelper.Pi - MathHelper.PiOver2;
                body.SetRestitution(1.0f);
                balls.Add(new BallSprite(body, radius));
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (var ball in balls) ball.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (var ball in balls) ball.Update(gameTime);
            world.Step(gameTime.ElapsedGameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (var ball in balls) ball.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

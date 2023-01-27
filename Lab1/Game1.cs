using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        CelAnimationPlayer animPlay;
        CelAnimationSequence animeSeq;

        Texture2D ForestBGTexture;
        private Rectangle ForestBGRectangle = new Rectangle();

        Texture2D WitchTexture;
        private Vector2 WitchDirection = new Vector2();
        private Rectangle WitchRectangle = new Rectangle();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            WitchDirection = new Vector2(1f ,0);


            base.Initialize();

            WitchRectangle = WitchTexture.Bounds;
            WitchRectangle = new Rectangle(Point.Zero, new Point(WitchRectangle.Width * 5, WitchRectangle.Height * 5));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ForestBGTexture = Content.Load<Texture2D>("ForestPreview");

            WitchTexture = Content.Load<Texture2D>("WitchRun");
            animeSeq = new CelAnimationSequence(WitchTexture, 32, 48, 1/ 8.0f);

            animPlay = new CelAnimationPlayer();
            animPlay.Play(animeSeq);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //animPlay.Play(animeSeq);

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                WitchDirection.X -= 10;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                WitchDirection.X += 10;
            }

            animPlay.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(ForestBGTexture, new Vector2(ForestBGRectangle.Location.X, -300), Color.White);


            animPlay.WitchDraw(_spriteBatch, new Vector2(300, 300), SpriteEffects.None);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
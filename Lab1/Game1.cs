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

        Texture2D PlayerTexture;
        private Vector2 PlayerPosition = new Vector2(300, 325);
        private SpriteEffects SE = SpriteEffects.None;
        private int spriterow = 1;
        private Vector2 PlayerDirection = new Vector2();
        private Rectangle PlayerRectangle = new Rectangle();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            PlayerRectangle = PlayerTexture.Bounds;
            PlayerRectangle = new Rectangle(Point.Zero, new Point(PlayerRectangle.Width * 5, PlayerRectangle.Height * 5));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ForestBGTexture = Content.Load<Texture2D>("ForestPreview");

            PlayerTexture = Content.Load<Texture2D>("player");
            animeSeq = new CelAnimationSequence(PlayerTexture, 48, 48, 1/ 12.0f);

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
                PlayerPosition.X -= 4;
                spriterow = 4;
                SE = SpriteEffects.FlipHorizontally;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlayerPosition.X += 4;
                spriterow = 4;
                if (SE == SpriteEffects.FlipHorizontally)
                {
                    SE = SpriteEffects.None;
                }
            }
            else
            {
                spriterow = 1;
            }

            animPlay.Update(gameTime, spriterow);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(ForestBGTexture, new Vector2(ForestBGRectangle.Location.X, -300), Color.White);


            animPlay.PlayDraw(_spriteBatch, PlayerPosition, SE);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
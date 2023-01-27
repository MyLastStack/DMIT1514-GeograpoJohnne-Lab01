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

        CelAnimationPlayer foxPlay;
        CelAnimationSequence foxseq;

        // Background
        Texture2D BackgroundTexture;
        private Rectangle BGRectangle = new Rectangle();

        // Player
        Texture2D PlayerTexture;
        private Vector2 PlayerPosition = new Vector2();
        private SpriteEffects SE = SpriteEffects.None; // To give the ability to change spriteeffects of the sprite
        private int playerSpriteRow = 1;
        private Rectangle PlayerRectangle = new Rectangle();

        // Fox
        Texture2D FoxTexture;
        private Vector2 FoxPos = new Vector2(); // Enough to show the fox in front of the player sprite
        private int foxSpriteRow = 1;
        private Rectangle FoxRectangle = new Rectangle();
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

            // Changing the game window size to be the same as the background
            // The background just has an unusually massive sky area that I could've photoshopped off
            _graphics.PreferredBackBufferWidth = BackgroundTexture.Width;
            _graphics.PreferredBackBufferHeight = BackgroundTexture.Height;
            _graphics.ApplyChanges();

            PlayerRectangle = PlayerTexture.Bounds;
            PlayerRectangle = new Rectangle(Point.Zero, new Point(PlayerRectangle.Width * 5, PlayerRectangle.Height * 5));
            // 180 is just a random number to put the character on a proper spot
            PlayerPosition = new Vector2(50, _graphics.PreferredBackBufferHeight - 180);

            FoxRectangle = FoxTexture.Bounds;
            FoxRectangle = new Rectangle(Point.Zero, default);
            FoxPos = new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight - 120);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            BackgroundTexture = Content.Load<Texture2D>("background");

            PlayerTexture = Content.Load<Texture2D>("player");
            animeSeq = new CelAnimationSequence(PlayerTexture, 48, 48, 1/ 12.0f);

            FoxTexture = Content.Load<Texture2D>("fox");
            foxseq = new CelAnimationSequence(FoxTexture, 32, 32, 1/ 7.0f);

            animPlay = new CelAnimationPlayer();
            foxPlay= new CelAnimationPlayer();
            animPlay.Play(animeSeq);
            foxPlay.Play(foxseq);
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
                playerSpriteRow = 4;
                SE = SpriteEffects.FlipHorizontally;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlayerPosition.X += 4;
                playerSpriteRow = 4;
                if (SE == SpriteEffects.FlipHorizontally)
                {
                    SE = SpriteEffects.None;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                PlayerPosition.Y -= 4;
                playerSpriteRow = 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                PlayerPosition.Y += 4;
                playerSpriteRow = 3;
            }
            else
            {
                playerSpriteRow = 1;
            }

            animPlay.Update(gameTime, playerSpriteRow);
            foxPlay.Update(gameTime, foxSpriteRow);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(BackgroundTexture, Vector2.Zero, Color.White);
            animPlay.Draw(_spriteBatch, PlayerPosition, 3f, SE);
            foxPlay.Draw(_spriteBatch, FoxPos, 2f, SpriteEffects.None);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
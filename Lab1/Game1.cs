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

        // Background
        Texture2D ForestBGTexture;
        private Rectangle ForestBGRectangle = new Rectangle();

        // Player
        Texture2D PlayerTexture;
        private Vector2 PlayerPosition = new Vector2();
        private SpriteEffects SE = SpriteEffects.None; // To give the ability to change spriteeffects of the sprite
        private int playerSpriteRow = 1;
        private Rectangle PlayerRectangle = new Rectangle();

        // Fox
        Texture2D FoxTexture;
        private Vector2 FoxPos = new Vector2(0, 178); // Enough to show the fox in front of the player sprite
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
            _graphics.PreferredBackBufferWidth = ForestBGTexture.Width;
            _graphics.PreferredBackBufferHeight = ForestBGTexture.Height;
            _graphics.ApplyChanges();

            PlayerRectangle = PlayerTexture.Bounds;
            PlayerRectangle = new Rectangle(Point.Zero, new Point(PlayerRectangle.Width * 5, PlayerRectangle.Height * 5));
            // 180 is just a random number to put the character on a proper spot
            PlayerPosition = new Vector2(50, _graphics.PreferredBackBufferHeight - 180);

            
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
            else
            {
                playerSpriteRow = 1;
            }

            animPlay.Update(gameTime, playerSpriteRow);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(ForestBGTexture, Vector2.Zero, Color.White);
            animPlay.PlayDraw(_spriteBatch, PlayerPosition, SE);


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameFontsFun
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;

        // Example sentence.
        string sentence = "Hello, world!";

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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Make sure you've added a corresponding .spritefont file to your Content project.
            spriteFont = Content.Load<SpriteFont>("MonoGameCommunityFont1");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            DrawSentenceWithEnlargedFirstLetter(sentence, new Vector2(100, 100), spriteFont);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws a sentence where the first letter is drawn enlarged,
        /// with its bottom aligned with the baseline of the subsequent text (defined by font.LineSpacing),
        /// and appropriate spacing is maintained.
        /// </summary>
        /// <param name="text">The full sentence to draw.</param>
        /// <param name="position">The top-left position where the normal line of text should start.</param>
        /// <param name="font">The SpriteFont used for both measuring and drawing.</param>
        private void DrawSentenceWithEnlargedFirstLetter(string text, Vector2 position, SpriteFont font)
        {
            if (string.IsNullOrEmpty(text))
                return;

            float normalScale = 1.0f;
            float enlargedScale = 2.0f; // Scale for the first letter

            string firstLetter = text.Substring(0, 1);
            string restOfText = text.Substring(1);

            Vector2 normalFirstLetterSize = font.MeasureString(firstLetter); // Size of the first letter at normal scale

            // --- First Letter Drawing ---
            // Origin for the first letter: its bottom-left corner (unscaled).
            Vector2 firstLetterOrigin = new Vector2(0, normalFirstLetterSize.Y);

            // Drawing position for the first letter:
            // X: Aligns with the overall text position.X.
            // Y: Calculated so the bottom of the scaled letter aligns with the line's baseline (position.Y + font.LineSpacing).
            Vector2 firstLetterDrawPosition = new Vector2(position.X, position.Y + font.LineSpacing);

            _spriteBatch.DrawString(
                font,
                firstLetter,
                firstLetterDrawPosition,
                Color.White,
                rotation: 0f,
                origin: firstLetterOrigin, // Use the bottom-left origin
                scale: enlargedScale,      // Apply the enlarged scale
                effects: SpriteEffects.None,
                layerDepth: 0f);

            // --- Rest of Text Drawing ---
            // Calculate the X offset for the rest of the text.
            // This should be the width of the first letter *after* it has been scaled.
            float restOfTextOffsetX = normalFirstLetterSize.X * enlargedScale;
            Vector2 restOfTextDrawPosition = new Vector2(position.X + restOfTextOffsetX, position.Y);

            _spriteBatch.DrawString(
                font,
                restOfText,
                restOfTextDrawPosition, // Positioned after the scaled first letter
                Color.White,
                rotation: 0f,
                origin: Vector2.Zero,   // Standard top-left origin for the rest of the text
                scale: normalScale,     // Normal scale for the rest of the text
                effects: SpriteEffects.None,
                layerDepth: 0f);
        }
    }
}
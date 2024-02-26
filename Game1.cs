using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lists_and_Randomization
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator;

        List<Texture2D> planetTextures;
        List<Rectangle> planetRects;

        Texture2D spaceTexture;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            generator = new Random();

            // Initialze 13 Retangles to draw
            int size;
            planetRects = new List<Rectangle>();
            for (int i = 0; i < 13; i++)
            {
                size = generator.Next(35, 51);
                planetRects.Add(new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth - 50), generator.Next(_graphics.PreferredBackBufferHeight - 50), size, size));

            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            spaceTexture = Content.Load<Texture2D>("Images/space_background");
            // Load textures into a list
            planetTextures = new List<Texture2D>();
            for (int i = 1; i <= 13; i++)
                planetTextures.Add(Content.Load<Texture2D>("Images/16-bit-planet" + i));
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

            _spriteBatch.Draw(spaceTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            for (int i = 0; i < planetRects.Count; i++)
                _spriteBatch.Draw(planetTextures[i], planetRects[i], Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
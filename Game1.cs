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

        MouseState mouseState, prevMouseState;

        Random generator;

        List<Texture2D> textures;
        List<Texture2D> planetTextures;


        List<Rectangle> planetRects;

        Texture2D spaceBackgroundTexture;

        int size;
        float seconds;
        float respawnTime;

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
            
            seconds = 0f;
            respawnTime = 3f;

            // Initialize 13 Rectangles to draw
            textures = new List<Texture2D>();
            planetTextures = new List<Texture2D>();
            planetRects = new List<Rectangle>();
            for (int i = 0; i < 30; i++)
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

            spaceBackgroundTexture = Content.Load<Texture2D>("Images/space_background");
            // Load textures into a list
            for (int i = 1; i <= 13; i++)
                textures.Add(Content.Load<Texture2D>("Images/16-bit-planet" + i));
            // Create List of random Textures that will be drawn (this must be done after we have loaded all 13 textures into our program)
            for (int i = 0; i < planetRects.Count; i++)
                planetTextures.Add(textures[generator.Next(textures.Count)]);
        
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                for(int i = 0; i < planetRects.Count; i++)
                {
                    if (planetRects[i].Contains(mouseState.Position))
                    {
                        planetRects.RemoveAt(i);
                        planetTextures.RemoveAt(i);
                        i--;
                    }
                }

            // Calculates number of seconds since timer started
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // After 3 seconds, a new planet is added
            if (seconds > respawnTime)
            {
                size = generator.Next(35, 51);
                planetTextures.Add(textures[generator.Next(textures.Count)]);
                planetRects.Add(new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth - 50), generator.Next(_graphics.PreferredBackBufferHeight - 50), size, size));

                // Restarts Timer
                seconds = 0f;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (planetRects.Count == 0)
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(spaceBackgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            for (int i = 0; i < planetRects.Count; i++)
                _spriteBatch.Draw(planetTextures[i], planetRects[i], Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PiDarts.Core;
using PiDarts.Core.DartboardReaders;
using PiDarts.Core.Entities;
using PiDarts.Core.Enums;
using PiDarts.Core.Interfaces;
using PiDarts.Core.Layout;
using System.IO;

namespace PiDarts.Core
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PiDartsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        DartGameManager dartGameManager;
        IDartboardReader dbReader;

        public PiDartsGame(IDartboardReader _dbReader)
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            dbReader = _dbReader;
            dartGameManager = new DartGameManager(graphics, dbReader);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            //Used for drawing
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            LayoutController.configureLayout(graphics.GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Bounds.Height, 0);
            LayoutController.loadContent(Content);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="_gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime _gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            // TODO: Add your update logic here
            dartGameManager.UpdateState();

            base.Update(_gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="_gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime _gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            dartGameManager.Draw(spriteBatch);
        
            spriteBatch.End();

            base.Draw(_gameTime);
        }

        public void SetUpNewGame(GameSelection gameSelection, int numPlayers)
        {
            dartGameManager.SetUpNewGame(gameSelection,numPlayers);
        }

    }
}

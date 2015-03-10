using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using PiDarts.Core;
using PiDarts.Core.Enums;
using PiDarts.Core.DartboardReaders;

namespace PiDarts.Core
{
    public class DartGameManager
    {

        //Settings for the current game we are playing
        public DartGame dartGame;
        private Player[] players;

        //Interfaces w/ dartboard
        private IDartboardReader dbReader;

        //Allows drawing to the screen
        private GraphicsDeviceManager graphics;

        public DartGameManager(GraphicsDeviceManager _graphics, IDartboardReader _dbReader)
        {
            graphics = _graphics;
            dbReader = _dbReader;
        }

        /// <summary>
        /// This should be calld before UpdateState().
        /// This method initializes all initial settings for a game.
        /// It can also be used to reset a game.
        /// </summary>
        public void SetUpNewGame(GameSelection _gameSelection, int _numPlayers)
        {

            dartGame = new DartGame(_gameSelection, dbReader);
            players = new Player[_numPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player { position = i, currentThrow = 0, name = String.Format("Player {0}", i) };
            }
            dartGame.SetUpNewGame(players);
        }
        /// <summary>
        /// This should be called in a loop to continuously update the state of the game.
        /// Nothing in this method should block.
        /// </summary>
        public void UpdateState()
        {
            dartGame.UpdateState();
        }
        /// <summary>
        /// This should be called in a loop to continuously to draw the 
        /// game entities to the screen. Nothing in this method should block.
        /// </summary>
        public void Draw(SpriteBatch _spriteBatch)
        {
            dartGame.Draw(_spriteBatch);
        }
    }
}


using System;
using PiDarts.Core.Enums;
using PiDarts.Core.Entities;
using PiDarts.Core.GameTypes;
using PiDarts.Core.DartboardReaders;
using Microsoft.Xna.Framework.Graphics;

namespace PiDarts.Core
{
	public class DartGame
	{
        
        //Gets input from the dartboard
        private IDartboardReader dbReader;

        //Game Constants
		const int maxThrowsPerPlayer = 3;
		const int maxPlayers = 4; //TODO: Make this game specific?

		//Tracks current player and throw
        private Player[] players;
		private Player currentPlayer;
        private Hit lastHit;

        //Store our game configuration
        private Enums.GameSelection gameSelection;
        public readonly IGameType gameType;

        public static GameState state { get; private set; } 

        public DartGame(Enums.GameSelection _gameSelection, IDartboardReader _dbReader)
        {
            gameSelection = _gameSelection;
            switch (_gameSelection)
            {
                case GameSelection.Game301:
                    gameType = new GameType301(); ;
                    break;
            }

            dbReader = _dbReader;
        }

        /// <summary>
        /// Synchronously checks to see if a new hit has been read.
        /// When a new hit is read, next hit values will be > 0.
        /// Does not block;
        /// </summary>
        public void SetUpNewGame(Player[] _players)
        {
            state = GameState.GAME_START;
            players = _players;
            gameType.TriggerSetUpNewGame(players);
        }

        /// <summary>
        /// This should be called in a loop to continuously update the state of the game.
        /// Nothing in this method should block.
        /// </summary>
        public void UpdateState()
        {
            if (state == GameState.GAME_OVER)
            {
                //For unit test purposes, continue to read input from dartboard
                //Perhaps we could automatically start a new game on the next hit?
                dbReader.ReadDartboardHit();
                return;
            }

            if (state == GameState.TURN_BEGIN) { 
                //Do something here, create an animation to run or something.
            }

            //Start player turn
            currentPlayer = gameType.GetCurrentPlayer();

            //Read dartboard hit
            lastHit = dbReader.ReadDartboardHit();

            if (lastHit.value < 0)
            {
                //do Nothing
            }
            else
            {
                state = gameType.TriggerDartHit(lastHit);
                //Respond to state of the game
                switch (state)
                {
                    case GameState.GAME_END_FINAL_WINNER:
                        state = GameState.GAME_OVER;
                        break;
                };
            }
        }

        /// <summary>
        /// This should be called in a loop to continuously to draw the 
        /// game entities to the screen. Nothing in this method should block.
        /// </summary>
        public void Draw(SpriteBatch _spriteBatch) { 
            gameType.Draw(_spriteBatch);
        }
    }
}


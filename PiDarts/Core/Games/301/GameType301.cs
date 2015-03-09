using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PiDarts.Core.Entities;
using PiDarts.Core.Layout;
using System;

namespace PiDarts.Core.GameTypes
{
    /// <summary>
    /// Represents game rules for a standard game of 301 ( http://www.nicedarts.com/how_to_301.html )
    /// </summary>
	public class GameType301 : IGameType
	{
		#region IGameType implementation
		public string Name {get{ return startingScore.ToString();}}
		public int MaxNumPlayers {get{ return 6; } }
        public int MaxNumberDartThrows { get { return 3; } }

        //Not a constant because we can use same class for 501,701,901,etc.
		private int startingScore;

        //Variables to track progress throughout the game
        private Player[] players;
        private ScoreEntity301[] scores;

        //We track players in an array to avoid logic need for circular list
        //Could change this to make the code more elegant.
        private int currentPlayerIndex = 0;
        private Hit lastHit;

		public GameType301(int _startingScore = 301){
            startingScore = _startingScore;
		}

        /// <summary>
        /// Returns the current player based on what has happened thus far in the game.
        /// </summary>
        public Player GetCurrentPlayer() {
            return players[currentPlayerIndex];
        }

        /// <summary>
        /// This should be triggered by a DartGame object.
        /// This method sets up a new game. It should also be able to be
        /// called in order to 'reset' a game.
        /// TODO: Implement a reset_start game state.
        /// </summary>
		public GameState TriggerSetUpNewGame(Player[] _players){

            if (_players == null)
            {
                throw new ArgumentException("Players array cannot be null");
            }
            if (_players.Length == 0)
            {
                throw new ArgumentException("Players array must contain at least 1 entry.");
            }
            players = _players;

            scores = new ScoreEntity301[players.Length];

            //Reset scores
            for (int i = 0; i < scores.Length; i++){
                scores[i] = new ScoreEntity301(i);
                scores[i].currentScore = startingScore;
                scores[i].lastValidScore = startingScore;
            }
            currentPlayerIndex = 0;

            return GameState.GAME_START;
		}

        /// <summary>
        /// This is called internally to set the next player.
        /// It is NOT called on every update, only when GameState
        /// meets certain criteria.
        /// It returs false if the next player couldn't be set, which signals
        /// that the game is over.
        /// </summary>
        private bool SetNextPlayer() {;

            //Store the original position to know when we've gone full circle.
            int originalPlayer = currentPlayerIndex;

            //Set it for the next player
            int i = players[currentPlayerIndex].position + 1;

            //If we are at the end of the list, start at the beginning
            if (i >= players.Length) {
                i = 0;
            }

            //scores[players[i].Position] should always be equal to 'i',
            //this just makes it more clear that the score is determined by
            //indexing the players position into the scores array.
            while (true) {
                if (scores[players[i].position].currentScore > 0)
                {
                    currentPlayerIndex = i;
                    return true;
                }
                else {
                    i++;
                    if (i >= players.Length) {
                        i = 0;
                    }
                }

                //Returning false signals to the Game object that there are no more valid players
                //Thus the game should be over.
                if (i == originalPlayer) {
                    return false;
                }
            }
        }

        /// <summary>
        /// This should be triggered by a DartGame object.
        /// This method calculates the resulting state of the game based on 
        /// dart hits.
        /// </summary>
		public GameState TriggerDartHit (Hit _hit)
		{
            Player currentPlayer = players[currentPlayerIndex];

            lastHit = _hit;
            //Calculate new score based off of hit
            scores[currentPlayer.position].currentScore = scores[currentPlayer.position].currentScore - (_hit.modifier * _hit.value);

            //Temp variable for gamestate
            GameState resultState;

			//Check for BUST
            if (scores[currentPlayer.position].currentScore < 0)
            {
                scores[currentPlayer.position].currentScore = scores[currentPlayer.position].lastValidScore;
                players[currentPlayerIndex].currentThrow = 0;
                SetNextPlayer();
                resultState = GameState.BUST_TURN_END;
			} 
			//Check for game ending condition
            else if (scores[currentPlayer.position].currentScore == 0)
            {
                if(SetNextPlayer()){
                    players[currentPlayerIndex].currentThrow = 0;
                    resultState = GameState.GAME_END_NEXT_WINNER;
                }
                else
                {
                    resultState = GameState.GAME_END_FINAL_WINNER;
                }
			} 
			//Continue Normally
			else {
                players[currentPlayerIndex].currentThrow++;

                //Check to see if the player has thrown all of their darts
                if (players[currentPlayerIndex].currentThrow >= MaxNumberDartThrows)
                {
                    //Reset this player's throws to 0
                    players[currentPlayerIndex].currentThrow = 0;
                    scores[currentPlayer.position].lastValidScore = scores[currentPlayer.position].currentScore;
                    SetNextPlayer();
                    resultState = GameState.OK_TURN_END;
                }
                else {
                    resultState = GameState.OK;
                }
			}
            return resultState;
		}

        /// <summary>
        ///  This method is a place holder for any cleanup we need to do at the end of a game.
        /// </summary>
		public GameState TriggerGameEnd ()
		{
			return GameState.GAME_END_FINAL_WINNER;
		}
        /// <summary>
        ///  This method is used to draw scores and animations to the screen.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch) {

            LayoutController.DrawScores(spriteBatch, scores);
            LayoutController.DrawLastHit(spriteBatch, lastHit);
        }
		#endregion
	}
}


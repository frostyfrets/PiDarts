using Microsoft.Xna.Framework.Graphics;
using PiDarts.Core.Enums;
using System;

namespace PiDarts.Core
{
    /// <summary>
    ///  Interface between DartGame object and game rules.
    ///  Used to compute state of game within user turns.
    /// </summary>
    public interface IGameType
    {
        /// <summary>
        /// Returns user readable name of game type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the maximum number of players allowed for this gametype
        /// </summary>
        int MaxNumPlayers { get; }

        /// <summary>
        /// Returns the player who currently up.
        /// </summary>
        Player GetCurrentPlayer();

        /// <summary>
        /// Initializes game state. Should be called when a new game is started.
        /// </summary>
        GameState TriggerSetUpNewGame(Player[] _players);

        /// <summary>
        /// Calculates new game state based on a dart hit.
        /// </summary>
        GameState TriggerDartHit(Hit _hit);

        /// <summary>
        /// Calculates game state when game ends
        /// Unused for now.
        /// </summary>
        GameState TriggerGameEnd();

        /// <summary>
        /// Draws gametype specific entities
        /// </summary>
        void Draw(SpriteBatch _spriteBatch);

    }
}


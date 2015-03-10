using System;


/// <summary>
///  At any point in time, the state of the dart game will be represented by
///  one of these enums.
/// </summary>
namespace PiDarts
{
    public enum GameState { TURN_END, GAME_END_FINAL_WINNER, GAME_END_LOSER, OK, GAME_END_NEXT_WINNER, BUST_TURN_END, OK_TURN_END, GAME_OVER, GAME_START, TURN_BEGIN }

}


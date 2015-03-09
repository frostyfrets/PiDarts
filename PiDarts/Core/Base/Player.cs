using System;

namespace PiDarts.Core
{
    /// <summary>
    /// Represents a player (one who throws the darts).
    /// </summary>
	public class Player
	{
		public string name { get; set; }
		public int position { get; set; }
        public int currentThrow {get; set;}
	}
}


using System;

namespace PiDarts.Core
{
    /// <summary>
    /// Represents a dartboard value.
    /// Modifier can be 1,2, or 3. (single, double, triple)
    /// Value can be 1-20,25 (Bullseye)
    /// </summary>
	public struct Hit 
	{
		public int modifier {get;set; }
		public int value { get; set; }
	}
}


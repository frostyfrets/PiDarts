using System;

namespace PiDarts.Core.DartboardReaders
{
    /// <summary>
    /// Communicates with dartboard
    /// </summary>
	public interface IDartboardReader
	{
         Hit ReadDartboardHit();

	}
}


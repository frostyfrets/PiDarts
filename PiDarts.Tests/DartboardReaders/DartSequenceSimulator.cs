using PiDarts.Core;
using PiDarts.Core.DartboardReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDarts.Test
{
    /// <summary>
    /// Can be used to test game logic. Every call to ReadDartBoardHit() will
    /// return the next dartthrow in the sequence as a HIT object.
    /// </summary>
    public class DartThrowSequenceSimulator : IDartboardReader
    {
        DartThrowSequence seq;

        //Keeps track of dart throw
        int indexThrow = 0;


        /// <summary>
        /// Returns next throw in sequence.
        /// </summary>
        public Hit ReadDartboardHit()
        {
            if (indexThrow >= seq.modifiers.Length)
            {
                throw new DartEndOfSequenceException();
            }
            Hit temp = new Hit { modifier = seq.modifiers[indexThrow], value = seq.values[indexThrow] };
            indexThrow++;
            return temp;
        }
        public DartThrowSequenceSimulator(DartThrowSequence _seq)
        {
            seq = _seq;
        }

    }

    /// <summary>
    /// Used during testing to signal the end of a throw sequence.
    /// </summary>
    public class DartEndOfSequenceException : Exception { }

    /// <summary>
    /// Represents a series of throws.
    /// Used to test game logic.
    /// Value of dart throw number 'N' in DartThrowSequence Foo = 
    /// Foo.modifiers[N] * Foo.values[N];
    /// modifiers represents single,double,triple
    /// value represents segment value. 1-20, 25
    /// </summary>
    public abstract class DartThrowSequence
    {
        public virtual GameState endState { get; set; }
        public virtual int numPlayers { get; set; }
        public virtual int[] modifiers { get; set; }
        public virtual int[] values { get; set; }
    }
}


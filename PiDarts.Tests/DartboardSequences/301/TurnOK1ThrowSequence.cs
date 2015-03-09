using PiDarts.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDarts.Tests.DartboardSequences
{
    public class TurnOK1ThrowSequence : DartThrowSequence
    {
        public override int numPlayers { get { return 1; } }
        public override int[] modifiers { get { return new int[] { 3 }; } set { } }
        public override int[] values { get { return new int[] { 16}; } set { } }
    }
}

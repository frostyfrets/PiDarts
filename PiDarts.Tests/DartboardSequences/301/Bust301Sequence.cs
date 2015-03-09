using PiDarts.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDarts.Tests.DartboardSequences
{
    public class Bust301Sequence : DartThrowSequence
    {
        public override int numPlayers { get { return 1; } }
        public override int[] modifiers { get { return new int[] { 3, 3, 3, 3, 3, 1 }; } set { } }
        public override int[] values { get { return new int[] { 20, 20, 20, 20, 20, 2 }; } set { } }
    }
}

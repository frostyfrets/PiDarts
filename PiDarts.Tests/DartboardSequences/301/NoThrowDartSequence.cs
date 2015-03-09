using PiDarts.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDarts.Tests.DartboardSequences
{
    public class NoThrowDartSequence : DartThrowSequence
    {
        public override int numPlayers { get { return 1; } }
        public override int[] modifiers { get { return new int[] { }; } set { } }
        public override int[] values { get { return new int[] { }; } set { } }
    }
}

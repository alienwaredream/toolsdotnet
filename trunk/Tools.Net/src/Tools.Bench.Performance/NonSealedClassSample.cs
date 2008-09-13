using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Bench.Performance
{
    class NonSealedClassSample
    {
        internal void DoWork() { int i = 0; i = i + 1; }
    }
}

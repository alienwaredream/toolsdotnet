using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Bench.Performance
{
    sealed class SealedClassSample
    {
        internal void DoWork() { int i = 0; i = i + 1; }
    }
}

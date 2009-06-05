using System;

namespace Tools.Coordination.Core
{
    [Serializable]
    public delegate void JobCompletedEventHandler
        (
        object sender,
        JobProcessedEventArgs e
        );
}
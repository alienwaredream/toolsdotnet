using Tools.Coordination.WorkItems;

namespace Tools.Coordination.Core
{
    public delegate void SubmittingJobDelegate
        (
        object sender,
        SubmittingJobEventArgs e
        );
}
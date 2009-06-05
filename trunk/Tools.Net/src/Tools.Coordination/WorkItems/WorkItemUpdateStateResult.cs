namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for WorkItemUpdateStateResult.
    /// </summary>
    public class WorkItemUpdateStateResult
    {
        private readonly long _assignmentToken;
        private readonly long _currentAssignmentToken;

        public WorkItemUpdateStateResult()
        {
        }

        public WorkItemUpdateStateResult
            (
            long currentAssignmentToken,
            long assignmentToken
            )
        {
            _currentAssignmentToken = currentAssignmentToken;
            _assignmentToken = assignmentToken;
        }

        public long CurrentAssignmentToken
        {
            get { return _currentAssignmentToken; }
        }

        public long AssignmentToken
        {
            get { return _assignmentToken; }
        }

        public AssignmentState AssignmentState
        {
            get
            {
                if (_currentAssignmentToken == 0) return AssignmentState.NoItem;
                if (_currentAssignmentToken != _assignmentToken) return AssignmentState.Reassigned;
                return AssignmentState.Valid;
            }
        }
    }
}
using System;

namespace Tools.Processes.Core
{
	/// <summary>
	/// Summary description for CompletionStatus.
	/// </summary>
	public enum ProcessCompletionStatus
	{
		/// <summary>
		/// Status can't be evaluated as Success or Failure.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// Completed successfuly.
		/// </summary>
		Success = 1,
		/// <summary>
		/// Assumes that not achieving success is a failure. Subject to review.
		/// If it is not a Failure, Unknown may be considered for use then. (SD).
		/// </summary>
		Failure = 2
	}
}

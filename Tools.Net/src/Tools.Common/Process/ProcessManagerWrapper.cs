using System;
using System.Diagnostics;

namespace Tools.Common.Process
{
	// TODO: This will be moved somewhere else!! (SD)
	/// <summary>
	/// Summary description for ProcessManagerWrapper.
	/// </summary>
	public class ProcessManagerWrapper : Process
	{


		public bool IsEmpty 
		{
			get 
			{
				return ProcessManager.Instance.IsEmpty;
			}
		}

		public ProcessManagerWrapper()
		{

		}

		public ProcessManagerWrapper(string name, string description)
			: base(name, description)
		{
		}


		#region IProcess Members

		public override void Start()
		{
			ProcessManager.Instance.Start();
		}

		public override void Abort()
		{
			ProcessManager.Instance.Abort();
		}
		public override void Stop()
		{
			ProcessManager.Instance.Stop();
		}

		#endregion


	}
}

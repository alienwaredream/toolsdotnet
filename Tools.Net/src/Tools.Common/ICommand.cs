using System;

namespace Tools.Common
{

	public interface ICommand
	{
		/// <summary>
		/// Execute method signature
		/// </summary>
		void Execute();
		/// <summary>
		/// Execute method signature
		/// </summary>
		void Execute(object context);
//		/// <summary>
//		/// UnExecute method signature
//		/// </summary>
//		void UnExecute();
	}
}

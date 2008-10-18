namespace Tools.UI.Windows.Descriptors
{
	public class ValueSelectedEventArgs<T> : System.EventArgs
	{
		private T _previousValue;
		private T _currentValue;

		public T PreviousValue
		{
			get { return _previousValue; }
			set { _previousValue = value; }
		}


		public T CurrentValue
		{
			get { return _currentValue; }
			set { _currentValue = value; }
		}

		public ValueSelectedEventArgs
			(
			T previousValue,
			T currentValue
			)
		{
			_previousValue = previousValue;
			_currentValue = currentValue;
		}

	}
}

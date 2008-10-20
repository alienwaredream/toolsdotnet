using System;

namespace Tools.UI.Windows.Descriptors
{
    public class ValueSelectedEventArgs<T> : EventArgs
    {
        public ValueSelectedEventArgs
            (
            T previousValue,
            T currentValue
            )
        {
            PreviousValue = previousValue;
            CurrentValue = currentValue;
        }

        public T PreviousValue { get; set; }


        public T CurrentValue { get; set; }
    }
}
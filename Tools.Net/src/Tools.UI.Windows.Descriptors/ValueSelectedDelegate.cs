namespace Tools.UI.Windows.Descriptors
{
    public delegate void ValueSelectedDelegate<T>
        (
        object sender,
        ValueSelectedEventArgs<T> e
        );
}
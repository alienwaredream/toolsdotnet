namespace Tools.UI.Windows.Descriptors
{
    internal interface IMarksAwareDomainsProvider<T> : IDomainsProvider<T>
    {
        string[] GetDomainValues(T obj, MarksPresentationType presentationType);
    }
}
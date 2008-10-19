namespace Tools.Core.Configuration
{
    public interface IConfigurationValueProvider
    {
        string this[string keyName] { get; }
    }
}
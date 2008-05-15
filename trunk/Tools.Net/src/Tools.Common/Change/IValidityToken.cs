
namespace Tools.Common.Change
{
    public interface IValidityToken<T>
    {
        void Next();
        void Reset();

        T TokenValue { get;}
    }
}

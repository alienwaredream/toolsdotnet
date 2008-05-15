using System;
using System.Threading;


namespace Tools.Common.Change
{
    [Serializable()]
    public class NumericValidityToken : IValidityToken<long>
    {
        private /*volatile*/ long _tokenValue = 0;
        // TODO: Reconsider use of volatile again, 
        // all access is anyway covered by Interlocked at the moment (SD)

        public NumericValidityToken()
        {
        }

        #region IValidityToken Members

        public long TokenValue
        {
            get
            {
                return Interlocked.Read(ref _tokenValue);
            }
            set
            {
                Interlocked.Exchange(ref _tokenValue, value);
            }
        }

        public void Next()
        {
            Interlocked.Increment(ref _tokenValue);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _tokenValue, 0);
        }

        #endregion
    }
}

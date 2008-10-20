using System.Collections;

namespace Tools.Processes.Core
{

    #region IProcessEnumerator class

    public class IProcessEnumerator : object, IEnumerator
    {
        #region Fields

        private readonly IEnumerator baseEnumerator;
        private readonly IEnumerable temp;

        #endregion

        #region Constructors

        public IProcessEnumerator(IProcessCollection mappings)
        {
            temp = ((mappings));
            baseEnumerator = temp.GetEnumerator();
        }

        #endregion

        #region Properties

        public IProcess Current
        {
            get { return ((IProcess) (baseEnumerator.Current)); }
        }

        #endregion

        #region IEnumerator implementation

        object IEnumerator.Current
        {
            get { return baseEnumerator.Current; }
        }

        bool IEnumerator.MoveNext()
        {
            return baseEnumerator.MoveNext();
        }

        void IEnumerator.Reset()
        {
            baseEnumerator.Reset();
        }

        #endregion

        #region Methods

        public bool MoveNext()
        {
            return baseEnumerator.MoveNext();
        }

        public void Reset()
        {
            baseEnumerator.Reset();
        }

        #endregion
    }

    #endregion
}
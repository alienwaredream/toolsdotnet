using System;
using System.Collections.Generic;

namespace Tools.Common.Change
{
    [Serializable()]
    public class GenericChangeEventArgs<T> : EventArgs where T : class
    {
        private T _oldState;
        private T _newState;
        private List<string> _categories = new List<string>();

        public T OldState
        {
            get
            {
                return _oldState;
            }
            set
            {
                _oldState = value;
            }
        }
        public T NewState
        {
            get
            {
                return _newState;
            }
            set
            {
                _newState = value;
            }
        }
        public List<string> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        public GenericChangeEventArgs()
        {

        }
        public GenericChangeEventArgs
            (
            T oldState,
            T newState)
            : this()
        {
            _oldState = oldState;
            _newState = newState;
        }
        public GenericChangeEventArgs
        (
        T oldState,
        T newState,
        List<string> categories
            )
            : this(oldState, newState)
        {
            _categories = categories;
        }

        public override string ToString()
        {
            string categoriesDump = null;
            _categories.ForEach(delegate(string val) { categoriesDump += val + ","; });
            return
                base.ToString() +
                ", OldState=" + ((_oldState == null) ? "null" : _oldState.ToString()) +
                ", NewState=" + ((_newState == null) ? "null" : _newState.ToString()) +
                ". Categories: " + categoriesDump;
        }

    }
}


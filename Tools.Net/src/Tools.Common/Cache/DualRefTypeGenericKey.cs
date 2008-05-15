using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Cache
{
    public struct DualGenericKey<KeyType> where KeyType : struct
    {
        private KeyType key1;
        private KeyType key2;

        public DualGenericKey(KeyType key1, KeyType key2)
        {
            this.key1 = key1;
            this.key2 = key2;
        }
        public override bool Equals(object obj)
        {
           if (obj == null) return false; 
           
            if (!(obj is DualGenericKey<KeyType>)) return false;

            DualGenericKey<KeyType> test = (DualGenericKey<KeyType>)obj;

            return (test.key1.Equals(key1) && test.key2.Equals(key2));
     
        }
        public override int GetHashCode()
        {
            return this.key1.GetHashCode() ^ this.key2.GetHashCode();
        }
    }
}

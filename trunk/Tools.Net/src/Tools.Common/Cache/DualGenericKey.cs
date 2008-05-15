using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Cache
{
    public struct DualRefTypeGenericKey<KeyType> where KeyType : class
    {
        private KeyType key1;

        /// <summary>
        /// Gets or sets the key1.
        /// </summary>
        /// <value>The key1.</value>
        public KeyType Key1
        {
            get { return key1; }
            set { key1 = value; }
        }
        private KeyType key2;

        /// <summary>
        /// Gets or sets the key2.
        /// </summary>
        /// <value>The key2.</value>
        public KeyType Key2
        {
            get { return key2; }
            set { key2 = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DualRefTypeGenericKey&lt;KeyType&gt;"/> class.
        /// </summary>
        /// <param name="key1">The key1.</param>
        /// <param name="key2">The key2.</param>
        public DualRefTypeGenericKey(KeyType key1, KeyType key2)
        {
            this.key1 = key1;
            this.key2 = key2;
        }
        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if obj and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
           if (obj == null) return false;

           if (!(obj is DualRefTypeGenericKey<KeyType>)) return false;

           DualRefTypeGenericKey<KeyType> test = (DualRefTypeGenericKey<KeyType>)obj;

            if (test.Key1 == null && this.key1 != null) return false;
            if (test.Key1 != null && this.key1 == null) return false;
            if (test.Key2 == null && this.key2 != null) return false;
            if (test.Key2 != null && this.key2 == null) return false;

            if (test.Key2 != null && !test.Key2.Equals(this.key2))
                return false;

            if (test.Key1 != null && !test.Key1.Equals(this.key1))
                return false;

            return true;
     
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            //TODO: Check through this implemenatation with stan and see if it needs
            // reviewing. mIkE...

            // Return hash code of keys based on which ones are not null

            // if Key2 is null
            if ((this.Key1 != null) && (this.Key2 == null))
            {
                return this.Key1.GetHashCode();
            }
            // if Key1 is null
            if ((this.Key1 == null) && (this.Key2 != null))
            {
                return this.Key2.GetHashCode();
            }

            // if both null
            if ((this.Key1 == null) && (this.Key2 == null))
            {
                return this.GetHashCode();
            }
            
            // if neither null
            return this.Key1.GetHashCode() ^ this.Key2.GetHashCode();            
        }
    }
}

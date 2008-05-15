using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Cache
{
    public delegate ItemType GetCachedItemCandidate<ItemType>(out bool useCache) where ItemType : class;


    /// <summary>
    /// Generic cache class.
    /// TODO: (SD) merge common with the GenericCacheProvider into base.
    /// </summary>
    /// <typeparam name="KeyType">The type of the key type.</typeparam>
    /// <typeparam name="ItemType">The type of the item type.</typeparam>
    /// <created by="SD" date="Mar-2007"/>
    public class GenericKeyedCacheProvider<KeyType, ItemType> /*: IGenericCacheProvider<KeyType, ItemType>*/
        where ItemType : class
    {


        private readonly int LockDurationMS = 1000;
        private Dictionary<KeyType, ItemType> items = new Dictionary<KeyType, ItemType>();
        
        private System.Threading.ReaderWriterLock rwLock = new System.Threading.ReaderWriterLock();



        public ItemType GetItem(KeyType key)
        {
            try
            {
                rwLock.AcquireReaderLock(LockDurationMS);

                ItemType item = null;

                items.TryGetValue(key, out item);
                
                return item;

            }
            finally
            {
                if (rwLock.IsReaderLockHeld) rwLock.ReleaseLock();
            }
            return null;
        }
        public ItemType GetOrAddItem(KeyType key,
            GetCachedItemCandidate<ItemType> getItemAction)
        {
            ItemType candidate = this.GetItem(key);

            if (candidate != null) return candidate;

            bool shouldCache = false;

            candidate = getItemAction(out shouldCache);

            if (shouldCache && candidate != null) AddItem(key, candidate);

            return candidate;
        }

        public void AddItem(KeyType key, ItemType item)
        {
            try
            {
                rwLock.AcquireWriterLock(LockDurationMS);

                ItemType itemCandidate = this.GetItem(key);

                if (item == null)
                {
                    items.Add(key, item);
                    return;
                }
                items.Remove(key);
                items.Add(key, item);
                return;

            }
            finally
            {
                if (rwLock.IsWriterLockHeld) rwLock.ReleaseLock();
            }
        }
    }
}

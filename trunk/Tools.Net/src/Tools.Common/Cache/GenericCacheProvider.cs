using System;
using System.Collections.Generic;
using System.Threading;

namespace Tools.Common.Cache
{
    /// <summary>
    /// Provides implementation of a cache with generic type of items stored.
    /// </summary>
    /// <typeparam name="ItemType">The type of the tem type.</typeparam>
    /// <created by="SD" date="Mar-2007"/>
    public class GenericCacheProvider<ItemType> where ItemType: class
    {
        private readonly int LockDurationMS = 1000;
        private List<ItemType> items = new List<ItemType>();
        private System.Threading.ReaderWriterLock rwLock = new System.Threading.ReaderWriterLock();

        #region Cache Reset implementation

        private static ReaderWriterLock _readerWriterLock =
            new ReaderWriterLock();

        private static int _validityToken = 0;

        #endregion Cache Reset implementation



        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="findPredicate">The find predicate.</param>
        /// <returns></returns>
        public ItemType GetItem(Predicate<ItemType> findPredicate)
        {
            try
            {
                rwLock.AcquireReaderLock(LockDurationMS);
                // items assignment to null should be protected by the 
                // _readerWriterLock
                CacheManager.ValidateCache<List<ItemType>>
                (
                _readerWriterLock,
                ref _validityToken,
                ref this.items
                );
                if (this.items == null)
                {
                    // not covering by any try/catch as really not expecting any error here,
                    // except for the line bellow, where then finally will cover it.
                    LockCookie lockCookie = rwLock.UpgradeToWriterLock(LockDurationMS);

                    items = new List<ItemType>();

                    rwLock.DowngradeFromWriterLock(ref lockCookie);

                    return default(ItemType);
                }
                return items.Find(findPredicate);
            }
            finally
            {
                if (rwLock.IsReaderLockHeld) rwLock.ReleaseLock();
            }
            return null;
        }

        /// <summary>
        /// Gets the or add item.
        /// </summary>
        /// <param name="findPredicate">The find predicate.</param>
        /// <param name="getItemAction">The get item action.</param>
        /// <returns></returns>
        public ItemType GetOrAddItem(Predicate<ItemType> findPredicate,
            GetCachedItemCandidate<ItemType> getItemAction)
        {
            ItemType candidate = this.GetItem(findPredicate);

            if (candidate != null) return candidate;

            bool shouldCache = false;

            candidate = getItemAction(out shouldCache);

            if (shouldCache && candidate != null) AddItem(candidate, findPredicate);

            return candidate;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="findPredicate">The find predicate.</param>
        public void AddItem(ItemType item, Predicate<ItemType> findPredicate)
        {
            try
            {
                rwLock.AcquireWriterLock(LockDurationMS);

                ItemType itemCandidate = items.Find(findPredicate);

                    if (item == null)
                    {
                        items.Add(item);
                        return;
                    }
                    items.Remove(itemCandidate);
                    items.Add(item);
                    return;

            }
            finally
            {
                if (rwLock.IsWriterLockHeld) rwLock.ReleaseLock();
            }
        }
    }
}

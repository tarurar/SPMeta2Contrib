using System.Collections.Generic;
using SPMeta2Contrib.Core.StoreProviders;

namespace SPMeta2Contrib.Core.Store
{
    public class PersistentDictionary<TKey, TValue>: Dictionary<TKey, TValue>, IDataStore
    {
        protected readonly IStoreProvider<IDictionary<TKey, TValue>> StoreProvider;

        public PersistentDictionary(IStoreProvider<IDictionary<TKey, TValue>> provider)
        {
            StoreProvider = provider;
        }

        public virtual void Load()
        {
            StoreProvider.Load(this);
        }

        public virtual void Save()
        {
            StoreProvider.Save(this);
        }
    }
}

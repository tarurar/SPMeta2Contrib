using System;
using System.Collections.Generic;

namespace SPMeta2Contrib.Core.StoreProviders
{
    public abstract class DictionaryStoreProvider<TKey, TValue>: IStoreProvider<IDictionary<TKey, TValue>>
    {
        public virtual void Load(IDictionary<TKey, TValue> container)
        {
            throw new NotImplementedException();
        }

        public virtual void Save(IDictionary<TKey, TValue> dataSource)
        {
            throw new NotImplementedException();
        }
    }
}

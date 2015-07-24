namespace SPMeta2Contrib.Core.Store
{
    public interface IHashStore<T>: IDataStore
    {
        byte[] Add(T objectToStoreHash);
        byte[] AddOrUpdate(T objectToStoreHash);
        bool Remove(T objectToRemove);
        bool HasSame(T objectToCheck);
    }
}
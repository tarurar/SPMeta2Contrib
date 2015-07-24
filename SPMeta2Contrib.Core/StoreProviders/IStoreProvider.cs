namespace SPMeta2Contrib.Core.StoreProviders
{
    public interface IStoreProvider<T>
    {
        void Load(T container);
        void Save(T dataSource);
    }
}

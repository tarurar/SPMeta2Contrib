using System.Collections.Generic;
using System.IO;
using SPMeta2Contrib.Core.StoreProviders;
using SPMeta2Contrib.Core.Hash;

namespace SPMeta2Contrib.Core.Store
{
    public class FileHashStore: IHashStore<string>
    {
        protected PersistentDictionary<string, string> Store;
        protected readonly IHasher<FileStream> Hasher;

        private byte[] ComputeHash(string filename)
        {
            using (var fs = File.OpenRead(filename))
            {
                return Hasher.Compute(fs);
            }
        }

        public FileHashStore(IStoreProvider<IDictionary<string, string>> storeProvider, IHasher<FileStream> hasher)
        {
            Store = new PersistentDictionary<string, string>(storeProvider);
            Hasher = hasher;
        }
        public byte[] Add(string objectToStoreHash)
        {
            var hash = ComputeHash(objectToStoreHash);
            Store.Add(objectToStoreHash, hash.ToHex());

            return hash;
        }
        public byte[] AddOrUpdate(string objectToStoreHash)
        {
            byte[] hash;

            var exists = Store.ContainsKey(objectToStoreHash);
            if (exists)
            {
                hash = ComputeHash(objectToStoreHash);
                Store[objectToStoreHash] = hash.ToHex();
            }
            else
            {
                hash = Add(objectToStoreHash);
            }

            return hash;
        }

        public bool HasSame(string objectToCheck)
        {
            var hash = ComputeHash(objectToCheck);

            return Store.ContainsKey(objectToCheck) && Store[objectToCheck] == hash.ToHex();
        }
        public bool Remove(string objectToRemove)
        {
            return Store.Remove(objectToRemove);
        }

        public void Load()
        {
            Store.Load();
        }

        public void Save()
        {
            Store.Save();
        }
    }
}

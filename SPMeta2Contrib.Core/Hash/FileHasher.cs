using System.IO;
using System.Security.Cryptography;

namespace SPMeta2Contrib.Core.Hash
{
    public class FileMd5Hasher: IHasher<FileStream>
    {
        protected HashAlgorithm Algorithm = MD5.Create();

        public byte[] Compute(FileStream objectToGetHash)
        {
            return Algorithm.ComputeHash(objectToGetHash);
        }
    }
}
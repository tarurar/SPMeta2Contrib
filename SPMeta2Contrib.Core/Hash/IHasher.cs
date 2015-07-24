namespace SPMeta2Contrib.Core.Hash
{
    public interface IHasher<T>
    {
        byte[] Compute(T objectToGetHash);
    }
}
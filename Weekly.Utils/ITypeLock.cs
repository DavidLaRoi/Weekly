using System;
using System.Threading.Tasks;

namespace Weekly.Utils
{
    public interface ITypeLock
    {
        Task<IDisposable> LockAsync<T>();
    }

}
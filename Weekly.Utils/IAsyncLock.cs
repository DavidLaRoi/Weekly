using System;
using System.Threading;
using System.Threading.Tasks;

namespace Weekly.Utils
{
    public interface IAsyncLock
    {
        Task<IDisposable> LockAsync();
    }

}
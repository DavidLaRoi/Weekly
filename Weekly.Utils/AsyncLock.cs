using System;
using System.Threading;
using System.Threading.Tasks;

namespace Weekly.Utils
{
    public class AsyncLock : IAsyncLock, IDisposable
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public void Dispose()
        {
            ((IDisposable)semaphore).Dispose();
        }

        public Task<IDisposable> LockAsync()
        {
            return semaphore.WaitAsync().ContinueWith((x) => Extensions.DoAtDispose(semaphore.Release));
        }
    }
}

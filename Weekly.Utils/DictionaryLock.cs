using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Weekly.Utils
{
    public class DictionaryLock<TKey>
    {
        private ConcurrentDictionary<TKey, IAsyncLock> locks = new ConcurrentDictionary<TKey, IAsyncLock>();

        public Task<IDisposable> LockAsync(TKey key) 
        {
            return locks.GetOrAdd(key, x => new AsyncLock()).LockAsync();
        }

    }

}
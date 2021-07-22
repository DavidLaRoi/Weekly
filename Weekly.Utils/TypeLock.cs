using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weekly.Utils
{

    public class TypeLock : 
        DictionaryLock<Type>, 
        ITypeLock
    {
        public Task<IDisposable> LockAsync<T>()
        {
            return base.LockAsync(typeof(T));
        }
    }

}
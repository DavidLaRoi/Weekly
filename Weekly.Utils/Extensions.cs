using System;
using System.Linq;
using System.Threading.Tasks;

namespace Weekly.Utils
{
    public static class Extensions
    {
        public static object[] Arr(params object[] vs) => vs;

        public static IDisposable DoAtDispose(Action action)
        {
            return new DoAtDisposeCls(action);
        }

        public static IDisposable DoAtDispose<T>(Func<T> action)
        {
            return new DoAtDisposeCls(() => action());
        }

        private class DoAtDisposeCls : IDisposable
        {
            private Action action;

            public DoAtDisposeCls(Action action)
            {
                this.action = action;
            }

            public void Dispose()
            {
                action?.Invoke();
                action = null;
            }
        }

        private class DoAtDisposeAsyncCls : IAsyncDisposable
        {
            private Func<Task> func;
            private readonly object lockObj = new object();

            public DoAtDisposeAsyncCls(Func<Task> func)
            {
                this.func = func;
            }

            public ValueTask DisposeAsync()
            {
                lock (this)
                {
                    if (func != null)
                    {
                        var task = func.Invoke();
                        func = null;
                        return new ValueTask(task);
                    }
                    else
                    {
                        return new ValueTask(Task.CompletedTask);
                    }
                }
            }
        }

        public static IAsyncDisposable DoAtDisposeAsync(Func<Task> func)
        {
            return new DoAtDisposeAsyncCls(func);
        }

        /// <summary>
        /// Initializes a 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        [Obsolete("messy")]
        public static T AutoWire<T> (this IServiceProvider serviceProvider)
        {
            var constr = typeof(T).GetConstructors().Single();
            var ps = constr.GetParameters().Select((x) => serviceProvider.GetService(x.ParameterType) ?? throw new Exception()).ToArray();
            return (T)constr.Invoke(ps);
        }
    }
}

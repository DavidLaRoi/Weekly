using System;
using System.Collections.Generic;
using System.Text;

namespace Weekly.Utils
{
    public static class Extensions
    {
        public static object[] Arr(params object[] vs) => vs;

        public static IDisposable DoAtDispose(Action action)
        {
            return new DoAtDisposeCls(action);
        }

        private class DoAtDisposeCls : IDisposable
        {
            Action action;

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

    }
}

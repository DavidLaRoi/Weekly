﻿using System;
using System.Linq;

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

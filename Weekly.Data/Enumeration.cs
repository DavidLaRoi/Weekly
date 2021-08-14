using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Weekly.Data
{
    public class Enumeration<T> where T : Enumeration<T>
    {
        #region Static

        private static IEnumerable<T> GetEnumerationsByReflection()
        {
            foreach(var field in typeof(T).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                if(field.GetType() == typeof(T))
                {
                    yield return (T)field.GetValue(null);
                }
            }
        }

        private static readonly Lazy<List<T>> enumerations = new Lazy<List<T>>(() => GetEnumerationsByReflection().ToList());

        public static T FromValue(int value)
        {
            return enumerations.Value.Where(x => x.Value == value).FirstOrDefault();
        }

        public static T FromName(string name)
        {
            return enumerations.Value.Where(x => x.Name == name).FirstOrDefault();
        }

        public static IEnumerable<T> GetEnumerations()
        {
            return enumerations.Value;
        }

        #endregion

        #region Members

        public int Value { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        #endregion
    }
}

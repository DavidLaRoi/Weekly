using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Weekly.Data
{
    public class Enumeration<T> where T : Enumeration<T> , new()
    {
        #region Static

        private static List<T> enumerations = null;

        protected static T Create(int value, string name, string description = null)
        {
            var newEnum = new T()
            {
                Value = value,
                Name = name,
                Description = description
            };
            while(value >= enumerations.Count)
            {
                enumerations.Add(null);
            }
            enumerations[value] = newEnum;
            return newEnum;
        }

        private static void EnsureConstructed()
        {
            if(enumerations is null)
            {
                enumerations = new List<T>();
                RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
            }
        }

        public static T FromValue(int value)
        {
            EnsureConstructed();
            
            if (value >= enumerations.Count || value < 0) return null;
            return enumerations[value];
        }

        public static T FromName(string name)
        {
            EnsureConstructed();

            return enumerations.FirstOrDefault((x) => x.Name == name);
        }

        public static IEnumerable<T> GetEnumerations()
        {
            EnsureConstructed();

            return enumerations;
        }

        #endregion

        #region Members

        public int Value { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        #endregion
    }
}

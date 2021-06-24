using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Linq;
namespace Weekly.Data
{

    public class Tracker
    {
        private readonly Hashtable Values = new Hashtable();

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            return Values[propertyName] is T t ? t : default;
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            Values[propertyName] = value;
        }

        public override string ToString()
        {
            return $"{{{string.Join(", ",Values.OfType<DictionaryEntry>().Select((x) => $"{x.Key} = {x.Value}"))}}}";
        }
    }

    public class Activity
    {
    }

    public class ActivityGroup : Tracker
    {
        public string Name
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Description
        {
            get => Get<string>();
            set => Set(value);
        }
    }


}

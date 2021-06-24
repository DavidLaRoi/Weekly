using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Weekly.Utils;

namespace Weekly.Behaviour
{
    public class ChangeTrackingVM<T> : ViewModel<T>
    {
        private bool track = true;

        private void EnableTrack()
        {
            track = true;
        }

        protected IDisposable PauseTracking()
        {
            track = false;
            return Extensions.DoAtDispose(EnableTrack);
        }

        protected void AddProps(params string[] propertyNames)
        {
            foreach (string propName in propertyNames)
            {
                AddProp(propName);
            }
        }

        protected void AddProp(string propertyName)
        {
            Values.Add(propertyName, null);
        }

        private readonly ChangeTracker CT = new ChangeTracker();

        private readonly Dictionary<string, object> Values = new Dictionary<string, object>();

        private class PropertyChange : IChange
        {
            private readonly ChangeTrackingVM<T> parent;
            private readonly string propertyName;
            private readonly object oldValue;
            private readonly object newValue;

            public PropertyChange(ChangeTrackingVM<T> parent, string propertyName, object oldValue, object newValue)
            {
                this.parent = parent;
                this.propertyName = propertyName;
                this.oldValue = oldValue;
                this.newValue = newValue;
            }

            public void Redo()
            {
                parent.SetValue(propertyName, newValue);
            }

            public void Undo()
            {
                parent.SetValue(propertyName, oldValue);
            }
        }

        protected void TrackPropertyChange<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            T oldValue = GetValue<T>(propertyName);
            if (!Equals(oldValue, newValue))
            {
                Values[propertyName] = newValue;
                OnPropertyChanged(propertyName);
                if (track)
                {
                    CT.Track(new PropertyChange(this, propertyName, oldValue, newValue));
                }
            }
        }

        protected void SetValue(string propertyName, object value)
        {
            Values[propertyName] = value;
            OnPropertyChanged(propertyName);
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return Values[propertyName] is T val ? val : default;
        }


    }
}

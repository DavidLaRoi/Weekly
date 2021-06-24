using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace Weekly.Behaviour
{ 

    public class ViewModel<T> :  INotifyPropertyChanged
    {
        private T model;

        public T Model
        {
            get => model;
            set
            {
                if (ReferenceEquals(model, value))
                {
                    return;
                }

                if (model is T)
                {
                    UnhookModel(model);
                }

                model = value;

                if(model is T)
                {
                    HookModel(model);
                }

                AllPropertiesChanged();
            }
        }

        protected virtual void HookModel(T model)
        {

        }

        protected virtual void UnhookModel (T model)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void AllPropertiesChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }

    public class ChangeTrackingVM<T> : ViewModel<T>
    {
        public ChangeTrackingVM()
        {
            foreach (var prop in GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if(prop.CanWrite && prop.CanRead)
                {
                    Values.Add(prop.Name, null);
                }
            }
        }

        private ChangeTracker CT = new ChangeTracker();

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
            if(!Equals(oldValue, newValue))
            {
                Values[propertyName] = newValue;
                CT.Track(new PropertyChange(this, propertyName, oldValue, newValue));
                OnPropertyChanged(propertyName);
            }
        }

        protected void SetValue(string propertyName, object value)
        {
            Values[propertyName] = value;
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return Values[propertyName] is T val ? val : default;
        }

        protected override void HookModel(T model)
        {
            base.HookModel(model);
        }
    }

    public interface IChange
    {
        void Undo();
        void Redo();
    }

    public class ChangeTracker : IEnumerable<IChange>
    {
        private readonly List<IChange> changes = new List<IChange>();

        private int headIndex = -1;

        public void Reset()
        {
            changes.Clear();
            headIndex = -1;
        }

        public bool CanRedo => headIndex >= 0 && headIndex < changes.Count - 1;

        public bool CanUndo => headIndex >= 0;

        public void Track(Action undo, Action redo)
        {
            Track(new Change(undo, redo));
        }

        public void Track(IChange change)
        {
            if (headIndex >= 0 && headIndex < changes.Count - 1)
            {
                changes.RemoveRange(headIndex + 1, changes.Count - headIndex - 1);
            }
            changes.Add(change);
            headIndex += 1;
        }

        public void Undo()
        {
            if (CanUndo)
            {
                var head = changes[headIndex];
                head.Undo();
                headIndex--;
            }
        }

        public void Redo()
        {
            if (CanRedo)
            {
                var nextHead = changes[headIndex + 1];
                nextHead.Redo();
                headIndex++;
            }
        }

        public IEnumerator<IChange> GetEnumerator() => changes.OfType<IChange>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class Change : IChange
        {
            public readonly Action Undo;
            public readonly Action Redo;

            public Change(Action undo, Action redo)
            {
                Undo = undo;
                Redo = redo;
            }

            void IChange.Redo() => Redo();

            void IChange.Undo() => Undo();
        }

    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Weekly.Behaviour
{

    public class ViewModel<T> : INotifyPropertyChanged
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

                if (model is T)
                {
                    HookModel(model);
                }

                AllPropertiesChanged();
            }
        }

        protected virtual void HookModel(T model)
        {

        }

        protected virtual void UnhookModel(T model)
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
}

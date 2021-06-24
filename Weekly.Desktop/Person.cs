using Weekly.Behaviour;

namespace Weekly.Desktop
{
    public class Person
    {
        public string Name { get; set; } = "David";

        public string LastName { get; set; } = "la Roi";

        public int Age { get; set; } = 28;

    }

    public class PersonViewModel : ChangeTrackingVM<Person>
    {
        public PersonViewModel()
        {
            AddProps(
                nameof(Name),
                nameof(LastName),
                nameof(Age));
        }

        protected override void HookModel(Person model)
        {
            using (PauseTracking())
            {
                Name = model.Name;
                LastName = model.LastName;
                Age = model.Age;
            }
        }

        public string Name
        {
            get => GetValue<string>();
            set => TrackPropertyChange(value);
        }

        public string LastName
        {
            get => GetValue<string>();
            set => TrackPropertyChange(value);
        }

        public int Age
        {
            get => GetValue<int>();
            set => TrackPropertyChange(value);
        }
    }


}

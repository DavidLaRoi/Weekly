namespace Weekly.Data
{
    public class ActivityStatus : Enumeration<ActivityStatus>
    {
        public static readonly ActivityStatus Created   = new ActivityStatus() { Value = 10000, Name = nameof(Created) };

        public static readonly ActivityStatus Dropped   = new ActivityStatus() { Value = 20000, Name = nameof(Dropped) };

        public static readonly ActivityStatus Skipped   = new ActivityStatus() { Value = 30000, Name = nameof(Skipped) };

        public static readonly ActivityStatus Completed = new ActivityStatus() { Value = 40000, Name = nameof(Completed) };

        public static readonly ActivityStatus Deleted   = new ActivityStatus() { Value = 50000, Name = nameof(Deleted) };

    }
}

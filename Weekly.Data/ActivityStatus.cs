using System;
using System.Collections.Generic;
using System.Text;

namespace Weekly.Data
{
    public class ActivityStatus : Enumeration<ActivityStatus>
    {
        public static ActivityStatus Created = Create(0, nameof(Created));

        public static ActivityStatus Dropped = Create(1, nameof(Dropped));

        public static ActivityStatus Skipped = Create(2, nameof(Skipped));

        public static ActivityStatus Deleted = Create(3, nameof(Deleted));

    }

    public enum Status
    {
        Created,
        Dropped,
        Skipped,
        Deleted
    }
}

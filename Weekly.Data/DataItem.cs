using System;

namespace Weekly.Data
{
    public class DataItem
    {
        public override bool Equals(object obj)
        {
            return obj is DataItem other && other.ID == ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }

}

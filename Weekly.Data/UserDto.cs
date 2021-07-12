using System;

namespace Weekly.Data
{
    public class Dto
    {
        public override bool Equals(object obj)
        {
            return obj is Dto other && other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public Guid? Id { get; set; }

    }

    public class UserDto : Dto
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }

}

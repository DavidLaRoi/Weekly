using System;

namespace Weekly.Data.Dtos
{
    public class Dto
    {
        public Guid? Id { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Dto other && other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

}

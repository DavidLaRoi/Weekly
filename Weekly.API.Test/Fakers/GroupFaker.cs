using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Dtos;

namespace Weekly.API.Test.Fakers
{

    public class DtoFaker<TDto> : Bogus.Faker<TDto> where TDto : Data.Dtos.Dto
    {
        public DtoFaker()
        {
           // Ignore(nameof(Dto.Id));
        }
    }

    public class GroupFaker : DtoFaker<Data.Dtos.Group>
    {
        public GroupFaker()
        {
            RuleFor(x => x.Description, x => x.Lorem.Random.Words(10));
            RuleFor(x => x.Name, x => x.Name.Random.Word());
        }
        protected override void PopulateInternal(Group instance, string[] ruleSets)
        {
            base.PopulateInternal(instance, ruleSets);
        }
    }
}

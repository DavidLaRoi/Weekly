using AutoMapper;
using System.Linq;
using Weekly.DB.Models;

namespace Weekly.API.Application.Queries
{
    public class GroupsGetQuery : DtosQuery<Data.Dtos.Group>
    {
        public GroupsGetQuery(int skip, int take) : base(skip, take)
        {
        }
    }

    public class GroupsGetQueryHandler : DtosQueryHandler<GroupsGetQuery, Data.Dtos.Group, DB.Models.Group>
    {
        public GroupsGetQueryHandler(IQueryable<Group> entities, IMapper mapper) : base(entities, mapper)
        {
        }
    }
}

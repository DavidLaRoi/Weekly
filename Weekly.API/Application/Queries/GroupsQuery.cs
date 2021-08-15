using AutoMapper;
using System.Linq;
using Weekly.DB.Models;

namespace Weekly.API.Application.Queries
{
    public class GroupsQuery : DtosQuery<Data.Dtos.Group>
    {
        public GroupsQuery(int skip, int take) : base(skip, take)
        {
        }
    }

    public class GroupsQueryHandler : DtosQueryHandler<GroupsQuery, Data.Dtos.Group, DB.Models.Group>
    {
        public GroupsQueryHandler(IQueryable<Group> entities, IMapper mapper) : base(entities, mapper)
        {
        }
    }
}

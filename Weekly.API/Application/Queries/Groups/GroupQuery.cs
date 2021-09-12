using AutoMapper;
using System;
using System.Linq;
using Weekly.DB.Models;

namespace Weekly.API.Application.Queries
{
    public class GroupQuery : DtoQuery<Data.Dtos.Group>
    {
        public GroupQuery(Guid id) : base(id)
        {
        }
    }

    public class GroupQueryHandler : DtoQueryHandler<GroupQuery, Data.Dtos.Group, DB.Models.Group>
    {
        public GroupQueryHandler(IMapper mapper, IQueryable<Group> entities) : base(mapper, entities)
        {
        }
    }


}

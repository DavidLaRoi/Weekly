using AutoMapper;
using System;
using Weekly.DB.Models;

namespace Weekly.API.Application.Commands
{
    public class GroupDeleteCommand : DtoDeleteCommand
    {
        public GroupDeleteCommand(Guid id) : base(id)
        {
        }
    }

    public class GroupDeleteCommandHandler : DtoDeleteCommandHandler<GroupDeleteCommand, Group>
    {
        public GroupDeleteCommandHandler(WeeklyContext weeklyContext, IMapper mapper) : base(weeklyContext, mapper)
        {
        }
    }
}

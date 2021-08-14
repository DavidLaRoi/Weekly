using AutoMapper;
using Weekly.DB.Models;

namespace Weekly.API.Application.Commands
{
    public class GroupUpdateCommand : DtoUpdateCommand<Data.Dtos.Group>
    {
        public GroupUpdateCommand(Data.Dtos.Group dto) : base(dto)
        {
        }
    }

    public class GroupUpdateCommandHandler : DtoUpdateCommandHandler<GroupUpdateCommand, Data.Dtos.Group, DB.Models.Group>
    {
        public GroupUpdateCommandHandler(WeeklyContext weeklyContext, IMapper mapper) : base(weeklyContext, mapper)
        {
        }
    }
}
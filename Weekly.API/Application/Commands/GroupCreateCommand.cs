using AutoMapper;
using Weekly.DB.Models;

namespace Weekly.API.Application.Commands
{
    public class GroupCreateCommand : DtoCreateCommand<Data.Dtos.Group>
    {
        public GroupCreateCommand(Data.Dtos.Group dto) : base(dto)
        {
        }
    }

    public class GroupCreateCommandHandler : DtoCreateCommandHandler<GroupCreateCommand, Data.Dtos.Group, DB.Models.Entity>
    {
        public GroupCreateCommandHandler(WeeklyContext weeklyContext, IMapper mapper) : base(weeklyContext, mapper)
        {
        }
    }
}
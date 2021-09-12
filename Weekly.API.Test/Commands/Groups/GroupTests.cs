using AutoMapper;

namespace Weekly.API.Test.Commands.Groups
{
    public abstract class GroupTests<TRequest> : TestBed<TRequest, DB.Models.WeeklyContext>
        where TRequest : class
    {
        protected override void ConfigureMappers(IMapperConfigurationExpression mappers)
        {
            mappers.CreateMap<Data.Dtos.Group, DB.Models.Group>().ReverseMap();
        }
    }
}

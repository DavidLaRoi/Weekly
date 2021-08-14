using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Weekly.Data;
using Weekly.DB.Models;

namespace Weekly.API.Application.Commands
{
    public class DtoUpdateCommand<TDto> : IRequest<Result>
        where TDto : Data.Dtos.Dto
    {
        public DtoUpdateCommand(TDto dto)
        {
            Dto = dto;
        }

        public TDto Dto { get; }
    }



    public class DtoUpdateCommandHandler<TRequest, TDto, TEntity> : IRequestHandler<TRequest, Result>
        where TRequest : DtoUpdateCommand<TDto>
        where TDto : Data.Dtos.Dto
        where TEntity : Entity
    {
        private readonly WeeklyContext WeeklyContext;
        private readonly IMapper mapper;

        public DtoUpdateCommandHandler(WeeklyContext weeklyContext, IMapper mapper)
        {
            WeeklyContext = weeklyContext;
            this.mapper = mapper;
        }

        public virtual async Task<Result> Handle(TRequest request, CancellationToken cancellationToken)
        {
            DbSet<TEntity> set = WeeklyContext.Set<TEntity>();
            TEntity entity = await set.FirstOrDefaultAsync(x => x.Id == request.Dto.Id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException(request.Dto.Id, typeof(TEntity));
            }
            mapper.Map(request.Dto, entity);
            await WeeklyContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
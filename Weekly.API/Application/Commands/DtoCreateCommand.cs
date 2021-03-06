using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Weekly.Data;
using Weekly.DB.Models;

namespace Weekly.API.Application.Commands
{
    public class DtoCreateCommand<TDto> : IRequest<Result>
        where TDto : Data.Dtos.Dto
    {
        public DtoCreateCommand(TDto dto)
        {
            Dto = dto;
        }

        public TDto Dto { get; }
    }

    public interface GetDtoType
    {
        Type Gettope();
    }

    public interface GetDtoType<T> : GetDtoType
    {
        new Type Gettope() => typeof(T);
    }

    public class Poo : GetDtoType<int>
    {
        public Type Gettope()
        {
            throw new NotImplementedException();
        }
    }

    public class DtoCreateCommandHandler<TRequest, TDto, TEntity> : IRequestHandler<TRequest, Result>
        where TRequest : DtoCreateCommand<TDto>
        where TDto : Data.Dtos.Dto
        where TEntity : Entity, new()
    {
        private readonly WeeklyContext WeeklyContext;
        private readonly IMapper mapper;

        public DtoCreateCommandHandler(WeeklyContext weeklyContext, IMapper mapper)
        {
            WeeklyContext = weeklyContext;
            this.mapper = mapper;
        }

        public virtual async Task<Result> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request.Dto.Id != default)
            {
                throw new InvalidOperationException("Cannot create a new entity with a set id, it must be default/zero.");
            }

            DbSet<TEntity> set = WeeklyContext.Set<TEntity>();

            var entity = new TEntity();
            mapper.Map(request.Dto, entity);
            set.Add(entity);
            await WeeklyContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
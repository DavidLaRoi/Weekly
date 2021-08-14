using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weekly.Data;
using Weekly.DB.Models;
using Weekly.Utils;

namespace Weekly.API.Application.Queries
{
    public class DtoQuery<TDto> : IRequest<Result<TDto>>
    {
        public DtoQuery(Guid id)
        {
            Id = Assert.NotZero(id);
        }

        public Guid Id { get; }
    }

    public abstract class DtoQueryHandler<TRequest, TDto, TEntity> :
        IRequestHandler<TRequest, Result<TDto>>
        where TRequest : DtoQuery<TDto>
        where TDto : new()
        where TEntity : Entity
    {
        protected readonly IMapper mapper;
        protected readonly IQueryable<TEntity> entities;

        protected DtoQueryHandler(IMapper mapper, IQueryable<TEntity> entities)
        {
            this.mapper = mapper;
            this.entities = entities;
        }

        public virtual async Task<Result<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            TEntity entity = await entities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            TDto dto = new();
            mapper.Map(entity, dto);
            return Result.Success(dto);
        }
    }




}

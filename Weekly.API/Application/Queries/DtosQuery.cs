using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weekly.Data;
using Weekly.DB.Models;
using Weekly.Utils;

namespace Weekly.API.Application.Queries
{
    public class DtosQuery<TDto> : IRequest<Result<IEnumerable<TDto>>>
    {
        public DtosQuery(int skip, int take)
        {
            Skip = Assert.BiggerThanOrEquals(skip, 0, nameof(skip));
            Take = Assert.SmallerThanOrEquals(take, 1000, nameof(take));
        }

        public int Skip { get; }

        public int Take { get; }
    }

    public abstract class DtosQueryHandler<TRequest, TDto, TEntity> : IRequestHandler<TRequest, Result<IEnumerable<TDto>>>
    where TRequest : DtosQuery<TDto>
    where TEntity : Entity
    where TDto : Data.Dtos.Dto
    {
        private readonly IQueryable<TEntity> entities;
        private readonly IMapper mapper;

        protected DtosQueryHandler(IQueryable<TEntity> entities, IMapper mapper)
        {
            this.entities = entities;
            this.mapper = mapper;
        }

        protected virtual IQueryable<TEntity> BeforeSkipTake(IQueryable<TEntity> entities)
        {
            return entities;
        }

        protected virtual IQueryable<TEntity> AfterkipTake(IQueryable<TEntity> entities)
        {
            return entities;
        }

        public virtual async Task<Result<IEnumerable<TDto>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = entities;
            query = query.OrderBy(x => x.Id);
            query = BeforeSkipTake(query);
            query = query.Skip(request.Skip);
            query = query.Take(request.Take);
            query = AfterkipTake(query);
            List<TDto> dtos = await mapper.ProjectTo<TDto>(query).ToListAsync(cancellationToken);
            return Result.Success<IEnumerable<TDto>>(dtos);
        }
    }
}

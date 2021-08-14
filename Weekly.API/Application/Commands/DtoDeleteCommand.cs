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
    public class DtoDeleteCommand : IRequest<Result>
    {
        public DtoDeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }


    public class DtoDeleteCommandHandler<TRequest, TEntity> : IRequestHandler<TRequest, Result>
        where TRequest : DtoDeleteCommand
        where TEntity : Entity
    {
        private readonly WeeklyContext WeeklyContext;

        public DtoDeleteCommandHandler(WeeklyContext weeklyContext, IMapper mapper)
        {
            WeeklyContext = weeklyContext;
        }

        public virtual async Task<Result> Handle(TRequest request, CancellationToken cancellationToken)
        {
            DbSet<TEntity> set = WeeklyContext.Set<TEntity>();
            TEntity entity = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException(request.Id, typeof(TEntity));
            }
            set.Remove(entity);
            await WeeklyContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Weekly.DB.Models;

namespace Weekly.API.Controllers
{
    [Route("api/statics")]
    [ApiController]
    public class StaticsController : ControllerBase
    {
        private readonly IServiceProvider provider;

        public StaticsController(IServiceProvider provider)
        {
            this.provider = provider;
        }

        [HttpGet]
        public IEnumerable<Data.Priority> GetPriorities()
        {
            IServiceScope scope = provider.CreateScope();
            IServiceProvider prov = scope.ServiceProvider;
            WeeklyContext context = prov.GetRequiredService<WeeklyContext>();

            foreach (Priority prio in context.Priorities)
            {
                yield return new Data.Priority()
                {
                    Description = prio.Description,
                    Name = prio.Name,
                    Id = prio.Id
                };
            }
        }

        [HttpPut]
        public void PutPriority(Data.Priority priority)
        {
            IServiceScope scope = provider.CreateScope();
            IServiceProvider prov = scope.ServiceProvider;
            WeeklyContext context = prov.GetRequiredService<WeeklyContext>();

            Priority existing = context.Priorities.Where((x) => x.Id == priority.Id).FirstOrDefault();
            Priority target = existing ?? new Priority();

            target.Description = priority.Description;
            target.Name = priority.Name;

            if (existing is null)
            {
                context.Priorities.Add(target);
            }

            context.SaveChanges();
        }

        [HttpDelete]
        public void DeletePriority(Data.Priority priority)
        {
            IServiceScope scope = provider.CreateScope();
            IServiceProvider prov = scope.ServiceProvider;
            WeeklyContext context = prov.GetRequiredService<WeeklyContext>();

            if (context.Priorities.Select((x) => x.Id == priority.Id) is DB.Models.Priority prio)
            {
                context.Priorities.Remove(prio);
            }

            context.SaveChanges();
        }
    }
}

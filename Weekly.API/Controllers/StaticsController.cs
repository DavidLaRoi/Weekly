using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weekly.DB.Models;

namespace Weekly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticsController : ControllerBase
    {
        private IServiceProvider provider;

        public StaticsController(IServiceProvider provider)
        {
            this.provider = provider;
        }

        [HttpGet]
        public IEnumerable<Data.Priority> GetPriorities()
        {
            var scope = provider.CreateScope();
            var prov = scope.ServiceProvider;
            var context = prov.GetRequiredService<WeeklyContext>();

            foreach(var prio in context.Priorities)
            {
                yield return new Data.Priority()
                {
                    Description = prio.Description,
                    Name = prio.Name,
                    ID = prio.Id,
                    IconUrl = prio.IconUrl
                };
            }
        }

        [HttpPut]
        public void PutPriority(Data.Priority priority)
        {
            var scope = provider.CreateScope();
            var prov = scope.ServiceProvider;
            var context = prov.GetRequiredService<WeeklyContext>();

            var existing = context.Priorities.Where((x) => x.Id == priority.ID).FirstOrDefault();
            var target = existing ?? new Priority();

            target.Description = priority.Description;
            target.Name = priority.Name;
            target.IconUrl = priority.IconUrl;

            if(existing is null)
            {
                context.Priorities.Add(target);
            }

            context.SaveChanges();
        }

        [HttpDelete]
        public void DeletePriority(Data.Priority priority)
        {
            var scope = provider.CreateScope();
            var prov = scope.ServiceProvider;
            var context = prov.GetRequiredService<WeeklyContext>();

            if(context.Priorities.Select((x) => x.Id == priority.ID) is DB.Models.Priority prio)
            {
                context.Priorities.Remove(prio);
            }

            context.SaveChanges();
        }
    }
}

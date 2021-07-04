using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = Weekly.DB.Models;

namespace Weekly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private IServiceProvider provider;


        [HttpGet]
        public IEnumerable<Weekly.Data.Task> GetTasks(Guid? groupID = null)
        {
            using var scope = provider.CreateScope();
            var prov = scope.ServiceProvider;
            using var context = prov.GetRequiredService<Weekly.DB.Models.WeeklyContext>();

            IQueryable<Models.Task> tasks = context.Tasks.Include((x) => x.Group);
            
            if (groupID.HasValue) tasks = tasks.Where((x) => x.GroupId == groupID);

            foreach(var dbTask in tasks)
            {
                yield return new Data.Task()
                {
                    ID = dbTask.Id,
                    Name = dbTask.Name,
                    Description = dbTask.Description,
                    Group = new Data.Group() { ID = dbTask.Group.Id, Name = dbTask.Group.Name },
                };
            }
        }

        [HttpGet]
        public List<Data.Task> GetChildTasks(Guid taskId)
        {
            using var scope = provider.CreateScope();
            var prov = scope.ServiceProvider;
            using var context = prov.GetRequiredService<DB.Models.WeeklyContext>();

            var subt = (from cte in context.TaskCtes
                       join task in context.Tasks.Include((x) => x.Group)
                       on cte.ChildId equals task.Id
                       where cte.RootId == taskId
                       select new { cte, task }).ToList();

            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Models = Weekly.DB.Models;

namespace Weekly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IServiceProvider provider;

        public TaskController(IServiceProvider provider)
        {
            this.provider = provider;
        }

        [HttpGet]
        public Weekly.Data.Task GetTask(Guid guid)
        {
            IServiceScope scope = provider.CreateScope();
            IServiceProvider prov = scope.ServiceProvider;
            using Models.WeeklyContext context = prov.GetRequiredService<Weekly.DB.Models.WeeklyContext>();

            var t = context.Tasks.Find(guid);
            if (t is null) return null;
            return new Data.Task() { Id = t.Id, Description = t.Description, Name = t.Name, Duration = t.Duration };
        }

        //[HttpGet]
        //public async IEnumerable<Weekly.Data.Task> GetTasks(Guid? groupID = null)
        //{
        //    //using IServiceScope scope = provider.CreateScope();
        //    //IServiceProvider prov = scope.ServiceProvider;
        //    //using Models.WeeklyContext context = prov.GetRequiredService<Weekly.DB.Models.WeeklyContext>();

        //    //IQueryable<Models.Task> tasks = context.Tasks.Include((x) => x.);

        //    //if (groupID.HasValue)
        //    //{
        //    //    tasks = tasks.Where((x) => x.GroupId == groupID);
        //    //}

        //    //foreach (Models.Task dbTask in tasks)
        //    //{
        //    //    yield return new Data.Task()
        //    //    {
        //    //        ID = dbTask.Id,
        //    //        Name = dbTask.Name,
        //    //        Description = dbTask.Description,
        //    //        Group = new Data.Group() { ID = dbTask.Group.Id, Name = dbTask.Group.Name },
        //    //    };
        //    //}
        //    return null;
        //}

        //[HttpGet("GetChilds")]
        //public List<Data.Task> GetChildTasks(Guid taskId)
        //{
        //    using IServiceScope scope = provider.CreateScope();
        //    IServiceProvider prov = scope.ServiceProvider;
        //    using Models.WeeklyContext context = prov.GetRequiredService<DB.Models.WeeklyContext>();

        //    var subt = (from cte in context.TaskCtes
        //                join task in context.Tasks.Include((x) => x.Group)
        //                on cte.ChildId equals task.Id
        //                where cte.RootId == taskId
        //                select new { cte, task }).ToList();

        //    throw new NotImplementedException();
        //}
    }
}

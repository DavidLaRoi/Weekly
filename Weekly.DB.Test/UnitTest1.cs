using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Weekly.DB.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new Weekly.DB.Models.WeeklyContext();
            var q = (from task in context.Tasks.Include((x) => x.TaskHasTaskParentTasks)
                     where task.Name.ToLower().Contains("uber")
                     select task);
            var exp = q.Expression.ToString();
            var uberTask = q.FirstOrDefault();

           var children = uberTask.Children.ToList();

            var t = (from tt in context.Tasks.Include((x) => x.TaskHasTaskSubTask)
                     join yy in context.TaskCtes on tt.Id equals yy.ChildId
                     where yy.RootId == uberTask.Id
                     select tt
                     ).ToList();

            var parented = t.Where((x) => x.TaskHasTaskSubTask?.ParentTask == uberTask).ToList();

            var b = t.Append(uberTask).Where((x) => context.Tasks.Local.Contains(x)).ToList();
        }
    }
}

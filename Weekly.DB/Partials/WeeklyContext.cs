using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using static Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions;
using static Weekly.Utils.Extensions;

namespace Weekly.DB.Models
{
    public class WeeklyContext : WeeklyContextBase
    {
        private IConnectionStringProvider connectionStringProvider;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionStringProvider.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ValueResult<int>>(entity =>
            {
                entity.HasNoKey();
            });
        }

        private static IEnumerable<string> Args(int length)
        {
            for (int i = 0; i < length; i++) yield return $"{{{i}}}";
        }

        protected T GetResult<T>(object[] args, [CallerMemberName] string functionName = null)
        {
            var set = Set<ValueResult<T>>();
            var query = FormattableStringFactory.Create($"SELECT [dbo].[{functionName}] ({string.Join(",", Args(args.Length))}) as Value", args);
            return set.
                FromSqlInterpolated(
                    query).
                    FirstOrDefaultAsync().Result.Value;
        }

        /// <summary>
        /// Verifies that a childTask can be added to a parenttask in terms of recursion. 
        /// </summary>
        /// <param name="parentTask"></param>
        /// <param name="childTask"></param>
        /// <returns></returns>
        public bool VerifyChildTaskEntry(Task parentTask, Task childTask)
        {
            return GetResult<int>(Arr(parentTask.Id, childTask.Id)) == 0;
        }

        public WeeklyContext(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }
    }
}

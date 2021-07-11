using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Weekly.Utils.Extensions;

namespace Weekly.DB.Models
{
    //public class WeeklyContext : WeeklyContextBase
    //{
    //    private readonly IContextConfigurer dbConfigurer;

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        dbConfigurer?.OnConfiguring(optionsBuilder);

    //        //if (Database.EnsureCreated())
    //        //{
    //        //    //database is created, we need to add some function??.
    //        //}
    //        //this method replaces the default configuring method.
    //        //base.OnConfiguring(optionsBuilder);
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //        modelBuilder.Entity<ValueResult<int>>(entity =>
    //        {
    //            entity.HasNoKey();
    //        });
    //    }

    //    private static IEnumerable<string> Args(int length)
    //    {
    //        for (int i = 0; i < length; i++)
    //        {
    //            yield return $"{{{i}}}";
    //        }
    //    }

    //    protected T GetResult<T>(object[] args, [CallerMemberName] string functionName = null)
    //    {
    //        DbSet<ValueResult<T>> set = Set<ValueResult<T>>();
    //        FormattableString query = FormattableStringFactory.Create($"SELECT [dbo].[{functionName}] ({string.Join(",", Args(args.Length))}) as Value", args);
    //        return set.
    //            FromSqlInterpolated(
    //                query).
    //                FirstOrDefaultAsync().Result.Value;
    //    }

    //    /// <summary>
    //    /// Verifies that a childTask can be added to a parenttask in terms of recursion. 
    //    /// </summary>
    //    /// <param name="parentTask"></param>
    //    /// <param name="childTask"></param>
    //    /// <returns></returns>
    //    public bool VerifyChildTaskEntry(Task parentTask, Task childTask)
    //    {
    //        return GetResult<int>(Arr(parentTask.Id, childTask.Id)) == 0;
    //    }

    //    public WeeklyContext(IContextConfigurer dbConfigurer)
    //    {
    //        this.dbConfigurer = dbConfigurer;
    //    }
    //}
}

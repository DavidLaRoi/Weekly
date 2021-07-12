using System;
using Weekly.DB.Models;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace Weekly.API_Test
{

    public class ApiUnitTest : IDisposable
    {
        private readonly WeeklyContext weeklyContext;

        public ApiUnitTest(WeeklyContext weeklyContext)
        {
            weeklyContext = weeklyContext;
        }

        public void Dispose()
        {
            weeklyContext.Database.EnsureDeleted();
            weeklyContext.Dispose();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}

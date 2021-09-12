using Microsoft.Extensions.DependencyInjection;
using System;
using Weekly.DB.Models;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace Weekly.API.Test
{

    public class CrapTest 
    {
        public interface IA
        {
            public string Get();
        }

        public interface IB
        {
            public string Yeet();
        }

        public class Poop : IA, IB
        {
            public Poop(string value)
            {

            }

            public string Get() => "Okay";
            public string Yeet() => "Okay";
        }

        [Fact]
        
        public void TestDoubleInjection()
        {
            var coll = new ServiceCollection();
            coll.AddSingleton<string>("This is not gooed.");
            coll.AddSingleton<IA, Poop>();
            coll.AddSingleton<IB, Poop>();
            var prov = coll.BuildServiceProvider();

            var ia = prov.GetRequiredService<IA>();
            var ib = prov.GetRequiredService<IB>();
            Assert.Equal(ia.Get(), ib.Yeet());
            Assert.False(ReferenceEquals(ia, ib));
            string poop = null;
            Assert.Throws<NullReferenceException>(() => poop = poop.Trim());
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.DB.Models;
using Xunit;

namespace Weekly.API.Test.Commands.Groups
{

    public class GroupCreateTests : GroupTests<Application.Commands.GroupCreateCommandHandler>
    {

        [Fact]
        public async System.Threading.Tasks.Task GroupCreateTest()
        {
            await using(Arrange(out var context))
            {
                //do nothing?
            }
            var dto = Faking.GroupFaker.Generate();
            using(Act(out var handler))
            {
                await handler.Handle(new Application.Commands.GroupCreateCommand(dto), default);
                await handler.Handle(new Application.Commands.GroupCreateCommand(dto), default);

            }
            using (CreateScope())
            {
                var context = ServiceProvider.GetRequiredService<WeeklyContext>();
                var list = await context.Groups.ToListAsync();
                Assert.Collection(list,
                    x =>
                    {
                        Assert.Equal(x.Name, dto.Name);
                        Assert.Equal(x.Description, dto.Description);
                    });
            }
        }

        [Fact]        
        public async System.Threading.Tasks.Task GroupCreateFail()
        {
            var dto = Faking.GroupFaker.Generate();

            await using (Arrange(out var context))
            {
                var entity = ServiceProvider.GetRequiredService<IMapper>().Map<Data.Dtos.Group, Group>(dto);
                context.Groups.Add(entity);
            }
            using (Act(out var handler))
            {
                await Assert.ThrowsAsync<Exception>(async () =>
                {
                    await handler.Handle(new Application.Commands.GroupCreateCommand(dto), default);
                });               
            }
        }

    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weekly.Data;

namespace Weekly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : WeeklyController
    {
        public GroupController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet(nameof(GroupsGet))]
        [ProducesResponseType(typeof(Result<IEnumerable<Data.Dtos.Group>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<IEnumerable<Data.Dtos.Group>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GroupsGet(int skip, int take)
        {
            Application.Queries.GroupsGetQuery request = new Weekly.API.Application.Queries.GroupsGetQuery(skip, take);
            Result<IEnumerable<Data.Dtos.Group>> response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost(nameof(GroupAdd))]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GroupAdd(Data.Dtos.Group group)
        {
            Application.Commands.GroupCreateCommand request = new Weekly.API.Application.Commands.GroupCreateCommand(group);
            Result response = await mediator.Send(request);
            return Ok(response);
        }
    }

}

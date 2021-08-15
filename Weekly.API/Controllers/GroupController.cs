using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            Application.Queries.GroupsQuery request = new(skip, take);
            Result<IEnumerable<Data.Dtos.Group>> response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet(nameof(GroupGet))]
        [ProducesResponseType(typeof(Result<Data.Dtos.Group>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<Data.Dtos.Group>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GroupGet(Guid id)
        {
            Application.Queries.GroupQuery request = new(id);
            Result response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost(nameof(GroupCreate))]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GroupCreate(Data.Dtos.Group group)
        {
            Application.Commands.GroupCreateCommand request = new(group);
            Result response = await mediator.Send(request);
            return Ok(response);
        }


        [HttpPut(nameof(GroupUpdate))]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GroupUpdate(Data.Dtos.Group group)
        {
            Application.Commands.GroupUpdateCommand request = new(group);
            Result response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete(nameof(GroupDelete))]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GroupDelete(Guid id)
        {
            Application.Commands.GroupDeleteCommand request = new(id);
            Result response = await mediator.Send(request);
            return Ok(response);
        }
    }

}

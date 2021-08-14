using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Weekly.Data;

namespace Weekly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator mediator;

        public TaskController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(nameof(GetTask))]
        [ProducesResponseType(typeof(Result<Data.Dtos.Task>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetTask(Guid guid)
        {
            throw new NotImplementedException();
        }

    }

}

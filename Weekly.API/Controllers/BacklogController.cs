using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Weekly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacklogController : ControllerBase
    {
        [HttpGet(nameof(GetBacklogs))]
        public IEnumerable<Data.Dtos.Backlog> GetBacklogs()
        {
            yield break;
        }
    }
}

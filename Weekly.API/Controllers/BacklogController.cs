using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Weekly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacklogController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Data.Backlog> GetBacklogs()
        {
            yield break;
        }
    }
}

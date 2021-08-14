using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Weekly.API.Controllers
{
    public class WeeklyController : ControllerBase
    {
        protected readonly IMediator mediator;

        public WeeklyController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sample1.DTO.Requests.Orders;
using Sample1.DTO.Response.Orders;

namespace Sample1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RS_Create), 200)]
        public Task<RS_Create> Create([FromBody] RQ_Create request)
        {
            return _mediator.Send(request);
        }

        [HttpGet]
        [ProducesResponseType(typeof(RS_Get), 200)]
        public Task<RS_Get> Get([FromQuery] RQ_Get request)
        {
            return _mediator.Send(request);
        }
    }
}

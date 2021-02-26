using MediatR;
using Sample1.DTO.Response.Orders;

namespace Sample1.DTO.Requests.Orders
{
    public class RQ_Create : IRequest<RS_Create>
    {
        public string Name { set; get; }
        public double Price { set; get; }
    }
}

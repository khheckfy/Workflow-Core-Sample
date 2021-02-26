using MediatR;
using Sample1.DTO.Response.Orders;

namespace Sample1.DTO.Requests.Orders
{
    public class RQ_Get : IRequest<RS_Get>
    {
        public int Id { set; get; }
    }
}

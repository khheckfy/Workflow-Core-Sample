using System.Threading;
using System.Threading.Tasks;
using Sample1.DTO.Requests.Orders;
using Sample1.DTO.Response.Orders;

namespace Sample1.BusinessLogic.RequestHandlers.Orders
{
    public class CreateRequestHandler : BaseRequestHandler<RQ_Create, RS_Create>
    {
        protected override Task<RS_Create> InnerHandle(RQ_Create request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new RS_Create
            {
                OrderId = DataSource.Add(request.Name, request.Price),
            });
        }
    }
}

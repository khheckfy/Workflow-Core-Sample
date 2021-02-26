using System.Threading;
using System.Threading.Tasks;
using Sample1.DTO.Requests.Orders;
using Sample1.DTO.Response.Orders;

namespace Sample1.BusinessLogic.RequestHandlers.Orders
{
    public class GetRequestHandler : BaseRequestHandler<RQ_Get, RS_Get>
    {
        protected override Task<RS_Get> InnerHandle(RQ_Get request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new RS_Get
            {
                Order = DataSource.GetOrder(request.Id),
            });
        }
    }
}

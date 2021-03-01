using System.Threading;
using System.Threading.Tasks;
using Sample1.DTO.Requests.Orders;
using Sample1.DTO.Response.Orders;
using WorkflowCore.Interface;

namespace Sample1.BusinessLogic.RequestHandlers.Orders
{
    public class CreateRequestHandler : BaseRequestHandler<RQ_Create, RS_Create>
    {
        private readonly IWorkflowHost _workflowHost;

        public CreateRequestHandler(IWorkflowHost workflowHost)
        {
            _workflowHost = workflowHost;
        }

        protected override Task<RS_Create> InnerHandle(RQ_Create request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new RS_Create
            {
                OrderId = DataSource.Add(request.Name, request.Price),
            });
        }
    }
}

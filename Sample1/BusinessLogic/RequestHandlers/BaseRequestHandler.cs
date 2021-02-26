using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Sample1.BusinessLogic.RequestHandlers
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected abstract Task<TResponse> InnerHandle(TRequest request, CancellationToken cancellationToken);

        protected virtual Task Validate(TRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return Task.CompletedTask;
        }

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            await Validate(request);
            var result = await InnerHandle(request, cancellationToken);
            return result;
        }
    }
}

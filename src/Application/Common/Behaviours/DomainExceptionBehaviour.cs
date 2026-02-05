using Domain.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class DomainExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        private readonly ILogger<DomainExceptionBehaviour<TRequest, TResponse>> _logger;

        public DomainExceptionBehaviour(ILogger<DomainExceptionBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (DomainException ex)
            {
                var requestName = typeof(TRequest).Name;

                // Log niveau Warning (métier, pas crash technique)
                _logger.LogWarning(ex,
                    "Flexitask Domain rule violation for Request {Name} {@Request} - {Message}",
                    requestName, request, ex.Message);

              throw;
            }
        }
    }
}

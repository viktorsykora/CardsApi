using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Abstractions.Behaviours
{
    internal class QueryTimeLoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQuery
    {
        private readonly ILogger<QueryTimeLoggingPipelineBehaviour<TRequest, TResponse>> _logger;

        public QueryTimeLoggingPipelineBehaviour(ILogger<QueryTimeLoggingPipelineBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            _logger.LogInformation("Query {QueryName} executed in {ElapsedMilliseconds} ms", typeof(TRequest).Name, stopwatch.ElapsedMilliseconds);

            return response;
        }
    }
}
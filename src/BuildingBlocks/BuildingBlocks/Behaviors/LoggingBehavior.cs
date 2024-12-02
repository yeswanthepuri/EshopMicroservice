using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - Response {Response} - RequestedData -{RequestedData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            var timetaken = timer.Elapsed;
            if (timetaken.Seconds > 3)
            {
                logger.LogInformation("[PERFORMANCE] The request={Request} Timer -{Time}",
             typeof(TRequest).Name, timetaken.Seconds);
            }
            logger.LogInformation("[END] The request={Request} Response {Response}",
            typeof(TRequest).Name, typeof(TResponse).Name);
            return response;

        }
    }
}

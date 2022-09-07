using Framework.Core.Exceptions;
using Framework.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Framework.Core.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        private readonly ILogger _log = Log.ForContext<ErrorHandlerMiddleware>();
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await _next.Invoke(context);

                sw.Stop();

                WriteToLog(context, sw);
            }
            // Never caught, because `LogException()` returns false.
            catch (Exception ex) when (LogException(context, sw, ex))
            {

            }
            catch (Exception error)
            {
                var response = context.Response;
                if (response != null)
                {
                    response.ContentType = "application/json";
                    var message = string.Empty;

                    switch (error)
                    {
                        case PrometheusException _:
                            // custom application error
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            message = error
                                .Message
                                .Replace("You attempted to access the Value property for a failed result. A failed result has no Value. The error was:", "", StringComparison.InvariantCultureIgnoreCase);
                            break;

                        case KeyNotFoundException _:
                            // not found error
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;

                        case SecurityTokenException _:
                            // not found error
                            response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            message = error.Message;
                            break;

                        default:
                            // unhandled error
                            message = response.StatusCode == (int)HttpStatusCode.OK ? ((CSharpFunctionalExtensions.ResultFailureException)error).Error : Constants.Constants.Messages.Error.TryAgainV2;
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }

                    await response.WriteAsync(message.SerializeToJson().Value);

                    WriteToLog(context, sw);
                }
            }
        }

        private void WriteToLog(HttpContext context, Stopwatch sw)
        {
            var statusCode = context.Response?.StatusCode;
            var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

            var log = level == LogEventLevel.Error ? LogForErrorContext(context) : _log;
            log.Write(level, MessageTemplate, context.Request.Method, context.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds);
        }

        private bool LogException(HttpContext httpContext, Stopwatch sw, Exception ex)
        {
            sw.Stop();

            LogForErrorContext(httpContext)
                .Error(ex, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds);

            return false;
        }

        private ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var result = _log
                .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            if (request.HasFormContentType)
                result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

            return result;
        }
    }
}

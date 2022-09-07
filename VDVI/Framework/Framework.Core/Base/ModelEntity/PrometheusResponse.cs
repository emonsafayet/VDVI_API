using System;
using System.Net;
using CSharpFunctionalExtensions;
using Framework.Core.Exceptions;
using Newtonsoft.Json;

namespace Framework.Core.Base.ModelEntity
{
    public class PrometheusResponse
    {
        private const string SuccessMessage = "Operation completed successfully";

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public PrometheusResponse()
        {
        }

        public PrometheusResponse(object data, HttpStatusCode statusCode, string message)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }

        public static Result<PrometheusResponse> Create(PrometheusException ex)
        {
            var parsingStatus = ParseErrorMessage(ex.Message);

            var sfResponse = parsingStatus.IsSuccess
                ? parsingStatus.Value
                : Failure(ex.Message, null, ex.StatusCode).Value;

            return Result.Success(sfResponse);
        }

        public static Result<PrometheusResponse> Success(object data, string message = SuccessMessage, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return Result.Success(new PrometheusResponse(data, statusCode, message));
        }

        public static Result<PrometheusResponse> Failure(string message, object data = null, HttpStatusCode statusCode = HttpStatusCode.ExpectationFailed)
        {
            return Result.Failure<PrometheusResponse>(message);
        }

        private static Result<PrometheusResponse> ParseErrorMessage(string message)
        {
            PrometheusResponse sfResponse;
            try
            {
                sfResponse = JsonConvert.DeserializeObject<PrometheusResponse>(message);
            }
            catch (Exception)
            {
                return Result.Failure<PrometheusResponse>("Message does not contain ProResponse object");
            }

            return Result.Success(sfResponse);
        }
    }
}
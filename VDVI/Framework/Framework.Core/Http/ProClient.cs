using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Flurl.Http;
using Flurl.Http.Content;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Models.Config;
using Microsoft.Extensions.Options;

namespace Framework.Core.Http
{
    public class ProClient : IProClient
    {
        private readonly Lazy<string> _baseUrl;

        public ProClient(IOptions<AppConfigs> configuration)
        {
            _baseUrl = new Lazy<string>(configuration.Value.ServerGateWayBaseUrl);
        }

        public async Task<Result<PrometheusResponse>> PostAsync(object dto, string segment, string headerName = null, string headerValue = null)
        {
            var json = FlurlHttp.GlobalSettings.JsonSerializer.Serialize(dto);

            var content = new CapturedStringContent(json, "application/json-patch+json");

            if (string.IsNullOrWhiteSpace(headerName) && string.IsNullOrWhiteSpace(headerValue))
            {
                return await _baseUrl.Value
                    .WithHeader(Constants.Constants.HttpRequestHeader.ApiVersion.DefaultHeader, Constants.Constants.HttpRequestHeader.ApiVersion.DefaultVersion)
                    .AppendPathSegment(segment)
                    .PostAsync(content)
                    .ReceiveJson<PrometheusResponse>();
            }

            return await _baseUrl.Value
                .WithHeader(headerName, headerValue)
                .AppendPathSegment(segment)
                .PostAsync(content)
                .ReceiveJson<PrometheusResponse>();
        }

        public async Task<Result<PrometheusResponse>> GetJsonAsync(
            string segment,
            PaginationParameters paginationParameters = null,
            string headerName = null,
            string headerValue = null,
            string apiVersion = Constants.Constants.HttpRequestHeader.ApiVersion.DefaultVersion)
        {
            IFlurlRequest flUrlRequest = _baseUrl.Value.WithHeader(Constants.Constants.HttpRequestHeader.ApiVersion.DefaultHeader, apiVersion);

            if (!string.IsNullOrWhiteSpace(headerName) && !string.IsNullOrWhiteSpace(headerValue))
            {
                flUrlRequest = flUrlRequest.WithHeader(headerName, headerValue);
            }

            if (paginationParameters != null)
            {
                flUrlRequest = flUrlRequest.SetQueryParams(paginationParameters);
            }

            return await flUrlRequest.AppendPathSegment(segment).GetJsonAsync<PrometheusResponse>();
        }

        public async Task<Result<PrometheusResponse>> DeleteAsync(
            string segment,
            string headerName = null,
            string headerValue = null,
            string apiVersion = Constants.Constants.HttpRequestHeader.ApiVersion.DefaultVersion)
        {
            IFlurlRequest flUrlRequest = _baseUrl.Value
                .WithHeaders(new
                {
                    Accept = "application/json-patch+json"
                }).WithHeader(Constants.Constants.HttpRequestHeader.ApiVersion.DefaultHeader, apiVersion);

            if (!string.IsNullOrWhiteSpace(headerName) && !string.IsNullOrWhiteSpace(headerValue))
            {
                flUrlRequest = flUrlRequest.WithHeader(headerName, headerValue);
            }

            var response = await flUrlRequest.AppendPathSegment(segment).DeleteAsync().ReceiveJson<PrometheusResponse>();

            return response;
        }
    }
}
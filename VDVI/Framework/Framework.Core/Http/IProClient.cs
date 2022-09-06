using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;

namespace Framework.Core.Http
{
    public interface IProClient
    {
        Task<Result<PrometheusResponse>> PostAsync(object dto, string segment, string headerName = null, string headerValue = null);

        Task<Result<PrometheusResponse>> GetJsonAsync(
            string segment,
            PaginationParameters paginationParameters = null,
            string headerName = null, string
                headerValue = null,
            string apiVersion = Constants.Constants.HttpRequestHeader.ApiVersion.DefaultVersion);

        Task<Result<PrometheusResponse>> DeleteAsync(
            string segment,
            string headerName = null,
            string headerValue = null,
            string apiVersion = Constants.Constants.HttpRequestHeader.ApiVersion.DefaultVersion);

    }
}

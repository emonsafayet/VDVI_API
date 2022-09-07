using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Framework.Core.Utility.Interfaces
{
    public interface IImageProcessingUtility
    {
        string GetImageFormat(string base64ImageData);
        Task<Result<string>> Base64ToImage(string basePath, string segment, string imageNameWithOutExtension, string base64ImageData);
        Task<Result<string>> ImagePathToBase64(string imagePath);
    }
}
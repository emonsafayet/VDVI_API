using System;
using System.IO;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Framework.Core.Exceptions;
using Framework.Core.Utility.Interfaces;

namespace Framework.Core.Utility
{
    public class ImageProcessingUtility : IImageProcessingUtility
    {
        public string GetImageFormat(string base64ImageData)
        {
            return base64ImageData.Split(';')[0].Replace("data:image/", "");
        }

        /// <summary>
        /// Creates an image file from the base64 string value along with proper image format.
        /// </summary>
        /// <param name="basePath">Base path to save the file</param>
        /// <param name="segment">Add</param>
        /// <param name="imageNameWithOutExtension">Image name without extension</param>
        /// <param name="base64ImageData">base64ImageData</param>
        /// <returns>Relative path</returns>
        public async Task<Result<string>> Base64ToImage(string basePath, string segment, string imageNameWithOutExtension, string base64ImageData)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var fileFormat = base64ImageData.Split(';')[0].Replace("data:image/", "");

                    if (base64ImageData.Contains(","))
                    {
                        base64ImageData = base64ImageData.Substring(base64ImageData.IndexOf(",", StringComparison.InvariantCultureIgnoreCase) + 1);
                    }

                    var imageByteData = Convert.FromBase64String(base64ImageData);

                    var relativePath = string.IsNullOrWhiteSpace(segment) ?
                        Path.Combine($"{imageNameWithOutExtension}.{fileFormat}") :
                        Path.Combine(segment, $"{imageNameWithOutExtension}.{fileFormat}");

                    var imagePath = Path.Combine(basePath, relativePath);

                    await using var fs = new FileStream(imagePath, FileMode.CreateNew);
                    await fs.WriteAsync(imageByteData, 0, imageByteData.Length);

                    return Result.Success(relativePath);
                },
                exception => new TryCatchExtensionResult<Result<string>>
                {
                    DefaultResult = Result.Failure<string>($"Error message: {exception.Message}. Details: {exception.GetExceptionDetailMessage()}"),
                    RethrowException = false
                });
        }

        /// <summary>
        /// Provides base64 image data from image path
        /// </summary>
        /// <param name="imagePath">Profile the image path along with image name</param>
        /// <returns></returns>
        public async Task<Result<string>> ImagePathToBase64(string imagePath)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    string base64ImageData = string.Empty;

                    if (!string.IsNullOrWhiteSpace(imagePath))
                    {
                        if (File.Exists(imagePath))
                        {
                            var imageExtension = Path.GetExtension(imagePath);
                            base64ImageData = $"data:image/{imageExtension.Replace(".", "")};base64,{Convert.ToBase64String(await File.ReadAllBytesAsync(imagePath))}";
                        }
                    }

                    return Result.Success(base64ImageData);
                },
                exception => new TryCatchExtensionResult<Result<string>>
                {
                    DefaultResult = Result.Failure<string>($"Error message: {exception.Message}. Details: {exception.GetExceptionDetailMessage()}"),
                    RethrowException = false
                });
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Framework.Core.Extensions
{
    public static class DictionaryExtension
    {
        public static async Task<MemoryStream> CreateZipPackageAsync(this Dictionary<string, byte[]> itemsToZip)
        {
            await using var compressedFileStream = new MemoryStream();

            //Create an archive and store the stream in memory.
            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
            {
                foreach (var item in itemsToZip)
                {
                    //Create a zip entry for each attachment
                    var zipEntry = zipArchive.CreateEntry(item.Key);

                    //Get the stream of the attachment
                    await using var originalFileStream = new MemoryStream(item.Value);
                    await using var zipEntryStream = zipEntry.Open();

                    //Copy the attachment stream to the zip entry stream
                    originalFileStream.CopyTo(zipEntryStream);
                }
            }

            return compressedFileStream;
        }
    }
}

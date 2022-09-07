using System.IO;

namespace Framework.Constants
{
    public static partial class Constants
    {
        public struct ApplicationPaths
        {
            public static string ContentRootPath;
            public static string WebRootPath;

            public static string AppDataPath => Path.Combine(ContentRootPath, "AppData");
        }
    }
}

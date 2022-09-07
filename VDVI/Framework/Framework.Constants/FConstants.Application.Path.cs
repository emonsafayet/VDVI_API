using System;
using System.IO;

namespace Framework.Constants
{
    public static partial class Constants
    {
        public struct Application
        {
            public struct Paths
            {
                private static string BasePath => AppDomain.CurrentDomain.BaseDirectory;
                private static string AppDataPath => Path.Combine(BasePath, "AppData");
                public static string Images => Path.Combine(AppDataPath, "Images");
                public static string TicketFilesPath => Path.Combine(AppDataPath, "TicketFiles");
                public static string AirlinesDataPath => Path.Combine(AppDataPath, "AirlinesData");
                public static string ReportImages => Path.Combine(Images, "Report");
            }
        }
    }
}

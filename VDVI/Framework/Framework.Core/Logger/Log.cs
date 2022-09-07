using Framework.Core.IoC;
using Unity;

namespace Framework.Core.Logger
{
    /// <summary>
    /// Log based on NLog
    /// </summary>
    public static class Log
    {
        private static IProLogger _logger;
        public static IProLogger Logger
        {
            get
            {
                if (_logger != null)
                    return _logger;

                _logger = DependencyUtility.Container?.Resolve<IProLogger>();
                return _logger;
            }
        }
    }
}

using Sampaio.Domain.Contracts.Infra;

namespace Sampaio.Logging
{
    public class Logging4NetFactory
    {
        private static ILogger _logger;
        private static string _connectionStr;

        public static void InitializeLogFactory(ILogger logger,
            string connectionStr) =>
            (_logger, _connectionStr) = (logger, connectionStr);
        
        public static ILogger Logger => _logger;

        public static string ConnectionString => _connectionStr;
    }
}
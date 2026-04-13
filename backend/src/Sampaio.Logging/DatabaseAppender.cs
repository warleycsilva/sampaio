using System;
using System.Data;
using Dapper;
using log4net.Appender;
using log4net.Core;
using Microsoft.Data.SqlClient;

namespace Sampaio.Logging
{
    public class DatabaseAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            using var sqlConnection = new SqlConnection(Logging4NetFactory.ConnectionString);

            try
            {
                var sql = $@"
                    insert into Logs (Id, OccurredAt, Level, Logger, Message, Exception)
                    values (@Id, @OccurredAt, @Level, @Logger, @Message, @Exception)";


                var logger = loggingEvent?.LoggerName?.Length > 255
                    ? loggingEvent?.LoggerName?.Substring(0, 255)
                    : loggingEvent?.LoggerName;

                sqlConnection.Execute
                (
                    sql,
                    new
                    {
                        Id = Guid.NewGuid(),
                        OccurredAt = DateTime.UtcNow,
                        Level = loggingEvent?.Level?.DisplayName,
                        Logger = logger,
                        Message = loggingEvent?.MessageObject?.ToString(),
                        Exception = loggingEvent?.ExceptionObject?.ToString()
                    },
                    commandType: CommandType.Text
                );
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}

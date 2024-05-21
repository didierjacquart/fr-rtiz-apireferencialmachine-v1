using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace BDM.ReferencialMachine.DataAccess.Helper
{
    public static class RetryHelper
    {
        private const double TIME_BETWEEN_RETRY = 3000;
        public static AsyncRetryPolicy Retry(ILogger _logger) =>
            Policy
                .Handle<SqlException>()
                .Or<DbUpdateException>()
                .WaitAndRetryAsync(
                    retryCount: 1,
                    sleepDurationProvider: (i) => TimeSpan.FromMilliseconds(TIME_BETWEEN_RETRY),
                    onRetry: (exception, timespan) =>
                    {
                        _logger.LogWarning(exception.GetType().Name, exception.Message);
                    }
                );
    }
}

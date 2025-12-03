using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http;

namespace Deve.Api.Helpers
{
    /// <summary>
    /// RateLimiter creation.
    /// https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-8.0
    /// </summary>
    public static class RateLimiterHelper
    {
        /// <summary>
        /// Partition Key used for Unauthenticated requests.
        /// </summary>
        const string PartitionKeyAnonymous = "RateLimiterAnonymous";

        public static PartitionedRateLimiter<HttpContext> CreateRateLimiter()
        {
            return PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                // Use the User Name to apply the Authenticated limits by user.
                // We avoid using the IP because of "Creating partitions on client IP addresses makes the app vulnerable to Denial of Service Attacks which employ IP Source Address Spoofing"
                // If you want to apply the limits globally, change the partitionKey to a fixed value.
                string partitionKey = httpContext?.User?.Identity?.Name ?? PartitionKeyAnonymous;

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: partitionKey,
                    factory: key =>
                    {
                        if (key == PartitionKeyAnonymous)
                        {
                            // Unauthenticated: Allow a maximum of 20 request per minute without queue
                            return new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 20,
                                Window = TimeSpan.FromMinutes(1),
                                QueueLimit = 1
                            };
                        }
                        else
                        {
                            // Authenticated: Allow 60 requests per minute with a maximum of 5 items queued
                            return new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 60,
                                Window = TimeSpan.FromMinutes(1),
                                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                                QueueLimit = 5
                            };
                        }
                    });
            });
        }
    }
}

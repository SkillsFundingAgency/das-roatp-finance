﻿using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.RoatpFinance.Web.Settings;
using StackExchange.Redis;

namespace SFA.DAS.RoatpFinance.Web.StartupExtensions
{
    public static class DataProtectionStartupExtensions
    {
        public static IServiceCollection AddDataProtection(this IServiceCollection services, IWebConfiguration configuration, IHostingEnvironment environment)
        {
            if (!environment.IsDevelopment())
            {
                var redisConnectionString = configuration.SessionRedisConnectionString;
                var dataProtectionKeysDatabase = configuration.DataProtectionKeysDatabase;

                var redis = ConnectionMultiplexer.Connect($"{redisConnectionString},{dataProtectionKeysDatabase}");

                services.AddDataProtection()
                    .SetApplicationName("das-roatp-finance")
                    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");
            }
            return services;
        }
    }
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.RoatpFinance.Web
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var nlogConfigFileName = "nlog.config";
            var logger = NLogBuilder.ConfigureNLog(nlogConfigFileName).GetCurrentClassLogger();

            try
            {
                logger.Info("Starting up host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not start host");
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("https://localhost:45669")
                .UseNLog();
    }
}

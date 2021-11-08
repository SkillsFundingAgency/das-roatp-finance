using Microsoft.Azure.Services.AppAuthentication;
using SFA.DAS.RoatpFinance.Web.Settings;
using System;

namespace SFA.DAS.RoatpFinance.Web.Infrastructure.ApiClients.TokenService
{
    public class QnaTokenService : IQnaTokenService
    {
        private readonly IWebConfiguration _configuration;

        public QnaTokenService(IWebConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(Uri baseUri)
        {
            if (baseUri != null && baseUri.IsLoopback)
                return string.Empty;

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var generateTokenTask = azureServiceTokenProvider.GetAccessTokenAsync(_configuration.QnaApiAuthentication.Identifier);

            return generateTokenTask.GetAwaiter().GetResult();
        }
    }
}

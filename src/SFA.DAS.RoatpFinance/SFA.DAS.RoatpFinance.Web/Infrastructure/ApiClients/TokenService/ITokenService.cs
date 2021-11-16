using System;

namespace SFA.DAS.RoatpFinance.Web.Infrastructure.ApiClients.TokenService
{
    public interface ITokenService
    {
        string GetToken(Uri baseUri);
    }
}

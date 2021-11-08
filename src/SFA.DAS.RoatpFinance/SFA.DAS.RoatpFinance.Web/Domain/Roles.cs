using System.Security.Claims;

namespace SFA.DAS.RoatpFinance.Web.Domain
{
    public static class Roles
    {
        public const string RoleClaimType = "http://service/service";

        public const string ProviderRiskAssuranceTeam = "EPR";
        public const string RoatpFinancialAssessorTeam = "FHC";

        public static bool HasValidRole(this ClaimsPrincipal user)
        {
            return user.IsInRole(RoatpFinancialAssessorTeam);
        }
    }
}

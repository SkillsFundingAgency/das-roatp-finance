using SFA.DAS.DfESignIn.Auth.Enums;
using SFA.DAS.DfESignIn.Auth.Interfaces;

namespace SFA.DAS.RoatpFinance.Web.Settings
{
    public class CustomServiceRole : ICustomServiceRole
    {
        public string RoleClaimType => "http://schemas.portal.com/service";
        public CustomServiceRoleValueType RoleValueType  => CustomServiceRoleValueType.Code;
    }
}
﻿using SFA.DAS.AdminService.Common.Settings;

namespace SFA.DAS.RoatpFinance.Web.Settings
{
    public interface IWebConfiguration
    {
        string SessionRedisConnectionString { get; set; }

        string SessionCachingDatabase { get; set; }

        string DataProtectionKeysDatabase { get; set; }

        AuthSettings StaffAuthentication { get; set; }

        ManagedIdentityApiAuthentication RoatpApplicationApiAuthentication { get; set; }

        ManagedIdentityApiAuthentication QnaApiAuthentication { get; set; }

        string EsfaAdminServicesBaseUrl { get; set; }
        
        bool UseDfeSignIn { get; set; }
        string DfESignInServiceHelpUrl { get; set; }
    }
}

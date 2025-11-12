using Newtonsoft.Json;

namespace SFA.DAS.RoatpFinance.Web.Settings;

public class AuthSettings : IAuthSettings
{
    [JsonRequired] public string WtRealm { get; set; }

    [JsonRequired] public string MetadataAddress { get; set; }

    public string Role { get; set; }
}
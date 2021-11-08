using AutoMapper;
using SFA.DAS.RoatpFinance.Web.AutoMapperProfiles;

namespace SFA.DAS.RoatpFinance.Web.Infrastructure.AutoMapper
{
    public static class MappingStartup
    {
        public static void AddMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<RoatpFinancialSummaryExportItemProfile>();
            });
        }
    }
}

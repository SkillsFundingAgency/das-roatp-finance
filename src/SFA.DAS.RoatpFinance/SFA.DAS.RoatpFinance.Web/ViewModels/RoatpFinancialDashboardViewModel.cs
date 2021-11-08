using SFA.DAS.RoatpFinance.Web.ApplyTypes.Dashboard;
using SFA.DAS.RoatpFinance.Web.ViewModels.Paging;

namespace SFA.DAS.RoatpFinance.Web.ViewModels
{
    public class RoatpFinancialDashboardViewModel
    {
        public PaginatedList<RoatpFinancialSummaryItem> Applications { get; set; }
        public RoatpFinancialApplicationsStatusCounts StatusCounts { get; set; }
        public string SelectedTab { get; set; }
        public string SearchTerm { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Apply;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Dashboard;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Export;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SFA.DAS.RoatpFinance.Web.Infrastructure.ApiClients
{
    public interface IRoatpApplicationApiClient
    {
        Task<RoatpApply> GetApplication(Guid applicationId);
        Task<RoatpContact> GetContactForApplication(Guid applicationId);
        Task<FinancialReviewDetails> GetFinancialReviewDetails(Guid applicationId);

        Task<List<RoatpSequence>> GetRoatpSequences();

        Task<List<RoatpFinancialSummaryItem>> GetClosedFinancialApplications(string searchTerm, string sortColumn, string sortOrder);
        Task<List<RoatpFinancialSummaryItem>> GetClarificationFinancialApplications(string searchTerm, string sortColumn, string sortOrder);
        Task<List<RoatpFinancialSummaryItem>> GetOpenFinancialApplications(string searchTerm, string sortColumn, string sortOrder);
        Task<List<RoatpFinancialSummaryDownloadItem>> GetOpenFinancialApplicationsForDownload();
        Task<RoatpFinancialApplicationsStatusCounts> GetFinancialApplicationsStatusCounts(string searchTerm);

        Task StartFinancialReview(Guid applicationId, string reviewer);
        Task ReturnFinancialReview(Guid applicationId, FinancialReviewDetails financialReviewDetails);


        Task<bool> UploadClarificationFile(Guid applicationId, string userId, IFormFileCollection clarificationFiles);
        Task<bool> RemoveClarificationFile(Guid applicationId, string userId, string filename);
        Task<HttpResponseMessage> DownloadClarificationFile(Guid applicationId, string filename);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SFA.DAS.AdminService.Common.Infrastructure;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Apply;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Dashboard;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Export;
using SFA.DAS.RoatpFinance.Web.Infrastructure.ApiClients.TokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SFA.DAS.RoatpFinance.Web.Infrastructure.ApiClients
{
    public class RoatpApplicationApiClient : ApiClientBase<RoatpApplicationApiClient>, IRoatpApplicationApiClient
    {
        public RoatpApplicationApiClient(HttpClient httpClient, ILogger<RoatpApplicationApiClient> logger, IRoatpApplicationTokenService tokenService) : base(httpClient, logger)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenService.GetToken(_httpClient.BaseAddress));
        }

        public async Task<RoatpApply> GetApplication(Guid applicationId)
        {
            return await Get<RoatpApply>($"/Application/{applicationId}");
        }

        public async Task<RoatpContact> GetContactForApplication(Guid applicationId)
        {
            return await Get<RoatpContact>($"/Application/{applicationId}/Contact");
        }

        public async Task<FinancialReviewDetails> GetFinancialReviewDetails(Guid applicationId)
        {
            return await Get<FinancialReviewDetails>($"/Application/{applicationId}/FinancialReviewDetails");
        }

        public async Task<List<RoatpSequence>> GetRoatpSequences()
        {
            return await Get<List<RoatpSequence>>($"/roatp-sequences");
        }

        public async Task<List<RoatpFinancialSummaryItem>> GetClosedFinancialApplications(string searchTerm, string sortColumn, string sortOrder)
        {
            return await Get<List<RoatpFinancialSummaryItem>>($"/Financial/ClosedApplications?searchTerm={searchTerm}&sortColumn={sortColumn}&sortOrder={sortOrder}");
        }

        public async Task<List<RoatpFinancialSummaryItem>> GetClarificationFinancialApplications(string searchTerm, string sortColumn, string sortOrder)
        {
            return await Get<List<RoatpFinancialSummaryItem>>($"/Financial/ClarificationApplications?searchTerm={searchTerm}&sortColumn={sortColumn}&sortOrder={sortOrder}");
        }

        public async Task<List<RoatpFinancialSummaryItem>> GetOpenFinancialApplications(string searchTerm, string sortColumn, string sortOrder)
        {
            return await Get<List<RoatpFinancialSummaryItem>>($"/Financial/OpenApplications?searchTerm={searchTerm}&sortColumn={sortColumn}&sortOrder={sortOrder}");
        }

        public async Task<List<RoatpFinancialSummaryDownloadItem>> GetOpenFinancialApplicationsForDownload()
        {
            return await Get<List<RoatpFinancialSummaryDownloadItem>>($"/Financial/OpenApplicationsForDownload");
        }

        public async Task<RoatpFinancialApplicationsStatusCounts> GetFinancialApplicationsStatusCounts(string searchTerm)
        {
            return await Get<RoatpFinancialApplicationsStatusCounts>($"/Financial/StatusCounts?searchTerm={searchTerm}");
        }

        public async Task StartFinancialReview(Guid applicationId, string reviewer)
        {
            await Post($"/Financial/{applicationId}/StartReview", new { reviewer });
        }

        public async Task ReturnFinancialReview(Guid applicationId, FinancialReviewDetails financialReviewDetails)
        {
            await Post($"/Financial/{applicationId}/Grade", financialReviewDetails);
        }

        public async Task<bool> UploadClarificationFile(Guid applicationId, string userId, IFormFileCollection clarificationFiles)
        {
            var fileName = string.Empty;
            var content = new MultipartFormDataContent
            {
                { new StringContent(userId), "UserId" }
            };

            if (clarificationFiles != null && clarificationFiles.Any())
            {
                foreach (var file in clarificationFiles)
                {
                    fileName = file.FileName;
                    var fileContent = new StreamContent(file.OpenReadStream())
                    {
                        Headers =
                        {
                            ContentLength = file.Length, ContentType = new MediaTypeHeaderValue(file.ContentType)
                        }
                    };
                    content.Add(fileContent, file.FileName, file.FileName);
                }

                try
                {
                    var response = await _httpClient.PostAsync($"/Clarification/Applications/{applicationId}/Upload", content);

                    return response.StatusCode == HttpStatusCode.OK;
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex,
                        $"Error when submitting Clarification File update for Application: {applicationId} | Filename: {fileName}");
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> RemoveClarificationFile(Guid applicationId, string userId, string fileName)
        {
            try
            {
                var response = await Post($"/Clarification/Applications/{applicationId}/Remove", new { fileName, userId });

                return response == HttpStatusCode.OK;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex,
                    $"Error when submitting Clarification File removal for Application: {applicationId} | Filename: {fileName}");
                return false;
            }
        }

        public async Task<HttpResponseMessage> DownloadClarificationFile(Guid applicationId, string filename)
        {
            var response = await GetResponse($"/Clarification/Applications/{applicationId}/Download/{filename}");

            return response;
        }        
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.AdminService.Common.Infrastructure;
using SFA.DAS.AdminService.Common.Infrastructure.Firewall;
using SFA.DAS.QnA.Api.Types;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Apply;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Dashboard;
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
    public class QnaApiClient : ApiClientBase<QnaApiClient>, IQnaApiClient
    {
        public QnaApiClient(HttpClient httpClient, ILogger<QnaApiClient> logger, IQnaTokenService tokenService) : base(httpClient, logger)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenService.GetToken(_httpClient.BaseAddress));
        }

        public async Task<string> GetQuestionTag(Guid applicationId, string questionTag)
        {
            var response = await GetResponse($"Applications/{applicationId}/applicationData/{questionTag}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<string>();
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                var apiError = GetApiErrorFromJson(json);
                var apiErrorMessage = apiError?.Message ?? json;

                _logger.LogError($"Error in QnaApiClient.GetQuestionTag() - applicationId {applicationId} | questionTag : {questionTag} | StatusCode : {response.StatusCode} | ErrorMessage: { apiErrorMessage }");
                return null;
            }
        }

        public async Task<IEnumerable<Sequence>> GetSequences(Guid applicationId)
        {
            return await Get<List<Sequence>>($"Applications/{applicationId}/Sequences");
        }

        public async Task<Sequence> GetSequence(Guid applicationId, Guid sequenceId)
        {
            return await Get<Sequence>($"Applications/{applicationId}/Sequences/{sequenceId}");
        }

        public async Task<Sequence> GetSequenceBySequenceNo(Guid applicationId, int sequenceNo)
        {
            return await Get<Sequence>($"Applications/{applicationId}/Sequences/{sequenceNo}");
        }

        public async Task<IEnumerable<Section>> GetSections(Guid applicationId)
        {
            return await Get<List<Section>>($"Applications/{applicationId}/sections");
        }

        public async Task<IEnumerable<Section>> GetSections(Guid applicationId, Guid sequenceId)
        {
            return await Get<List<Section>>($"Applications/{applicationId}/Sequences/{sequenceId}/sections");
        }

        public async Task<Section> GetSection(Guid applicationId, Guid sectionId)
        {
            return await Get<Section>($"Applications/{applicationId}/sections/{sectionId}");
        }

        public async Task<Section> GetSectionBySectionNo(Guid applicationId, int sequenceNo, int sectionNo)
        {
            return await Get<Section>($"Applications/{applicationId}/sequences/{sequenceNo}/sections/{sectionNo}");
        }

        public async Task<HttpResponseMessage> DownloadFile(Guid applicationId, Guid sectionId, string pageId, string questionId, string filename)
        {
            var response = await GetResponse($"/applications/{applicationId}/sections/{sectionId}/pages/{pageId}/questions/{questionId}/download/{filename}");

            if (!response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var apiError = GetApiErrorFromJson(json);
                var apiErrorMessage = apiError?.Message ?? json;

                _logger.LogError($"Error in QnaApiClient.DownloadFile() - applicationId {applicationId} | questionId : {questionId} | filename : {filename} | StatusCode : {response.StatusCode} | ErrorMessage: { apiErrorMessage }");
            }

            return response;
        }

        private static ApiError GetApiErrorFromJson(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<ApiError>(json);
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}

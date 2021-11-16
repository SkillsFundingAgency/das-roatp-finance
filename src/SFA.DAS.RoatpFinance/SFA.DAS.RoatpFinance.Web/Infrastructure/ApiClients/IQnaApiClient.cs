using SFA.DAS.QnA.Api.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SFA.DAS.RoatpFinance.Web.Infrastructure.ApiClients
{
    public interface IQnaApiClient
    {
        Task<string> GetQuestionTag(Guid applicationId, string questionTag);

        Task<IEnumerable<Sequence>> GetSequences(Guid applicationId);
        Task<Sequence> GetSequence(Guid applicationId, Guid sequenceId);
        Task<Sequence> GetSequenceBySequenceNo(Guid applicationId, int sequenceNo);
        Task<IEnumerable<Section>> GetSections(Guid applicationId, Guid sequenceId);
        Task<Section> GetSection(Guid applicationId, Guid sectionId);
        Task<Section> GetSectionBySectionNo(Guid applicationId, int sequenceNo, int sectionNo);

        Task<HttpResponseMessage> DownloadFile(Guid applicationId, Guid sectionId, string pageId, string questionId, string fileName);
    }
}

using Microsoft.AspNetCore.Http;
using SFA.DAS.AdminService.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.RoatpFinance.Web.ViewModels
{
    public class RoatpFinancialClarificationViewModel : RoatpFinancialApplicationViewModel
    {
        public string Comments { get; set; }
        public IFormFileCollection FilesToUpload { get; set; }
        public string InternalComments { get; set; }
        public string ClarificationFile { get; set; }
        public List<ValidationErrorDetail> ErrorMessages { get; set; }
    }
}

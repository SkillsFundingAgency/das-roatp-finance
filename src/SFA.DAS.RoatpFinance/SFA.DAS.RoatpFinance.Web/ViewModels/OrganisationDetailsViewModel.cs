﻿using System;

namespace SFA.DAS.RoatpFinance.Web.ViewModels
{
    public class OrganisationDetailsViewModel
    {
        public string ApplicationReference { get; set; }
        public string ApplicationRoute { get; set; }

        public string Ukprn { get; set; }
        public string OrganisationName { get; set; }
        public DateTime? SubmittedDate { get; set; }

        public string ApplicationRouteShortText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ApplicationRoute))
                {
                    return string.Empty;
                }
                var index = ApplicationRoute.IndexOf(' ');
                if (index < 0)
                {
                    return ApplicationRoute;
                }
                return ApplicationRoute.Substring(0, index + 1);
            }
        }
    }
}

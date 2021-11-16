namespace SFA.DAS.RoatpFinance.Web.ApplyTypes
{
    public static class RoatpQnaConstants
    {
        public static class RoatpSequences
        {
            public static int YourOrganisation = 1;
            public static int FinancialEvidence = 2;
        }

        public static class RoatpSections
        {

            public static class YourOrganisation
            {
                public static int OrganisationDetails = 2;
                public static int DescribeYourOrganisation = 4;

                public static class PageIds
                {
                    public static string ParentCompanyCheck = "20";
                    public static string ParentCompanyDetails = "21";
                    public static string IcoNumber = "30";
                    public static string Website = "40";
                    public static string TradingForMain = "50";
                    public static string TradingForEmployer = "51";
                    public static string TradingForSupporting = "60";
                    public static string OrganisationTypeMainSupporting = "140";
                    public static string OrganisationTypeEmployer = "150";
                    public static string EducationalInstituteType = "160";
                    public static string PublicBodyType = "170";
                    public static string SchoolType = "180";
                    public static string OrganisationRegisteredESFA = "200";
                    public static string OrganisationFundedESFA = "210";
                }
            }

            public static class FinancialEvidence
            {
                public static int YourOrganisationsFinancialEvidence = 2;
                public static int YourUkUltimateParentCompanysFinancialEvidence = 3;
            }
        }

        public static class QnaQuestionTags
        {
            public const string HasParentCompany = "HasParentCompany";
        }
    }
}

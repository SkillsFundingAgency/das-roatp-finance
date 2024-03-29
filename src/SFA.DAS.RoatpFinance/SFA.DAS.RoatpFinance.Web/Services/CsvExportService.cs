﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Export;

namespace SFA.DAS.RoatpFinance.Web.Services
{
    public class CsvExportService : ICsvExportService
    {
        public byte[] WriteCsvToByteArray<T, TU>(IEnumerable<T> records) where TU : ClassMap<T>
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
            {
                csvWriter.Configuration.SanitizeForInjection = true;
                csvWriter.Configuration.RegisterClassMap<TU>();
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }
    }

    public sealed class RoatpFinancialSummaryExportCsvMap : ClassMap<RoatpFinancialSummaryExportItem>
    {
        public RoatpFinancialSummaryExportCsvMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(x => x.ApplicationId).Ignore();
            Map(x => x.ApplicationReference).Name("Application reference Id");
            Map(x => x.Route).Name("Provider route");
            Map(x => x.SubmissionDate).Name("Submitted date").TypeConverterOption.Format("yyyy-MM-dd");
            Map(x => x.GatewayCompletionDate).Name("Gateway complete date").TypeConverterOption.Format("yyyy-MM-dd");
            Map(x => x.ProviderName).Name("Provider legal name");
            Map(x => x.Ukprn).Name("UKPRN");
            Map(x => x.CompanyNo).Name("Company No");
            Map(x => x.CharityNo).Name("Charity No");
            Map(x => x.TurnOver).Name("Turnover");
            Map(x => x.Depreciation).Name("Depreciation/Amortisation");
            Map(x => x.ProfitLoss).Name("Profit/Loss");
            Map(x => x.Dividends);
            Map(x => x.IntangibleAssets).Name("Intangible assets");
            Map(x => x.Assets).Name("Total current assets");
            Map(x => x.Liabilities).Name("Total current liabilities");
            Map(x => x.ShareholderFunds).Name("Shareholder funds/Net assets");
            Map(x => x.Borrowings).Name("Total borrowings");
            Map(x => x.AccountingReferenceDate).Name("Accounting reference date").TypeConverterOption.Format("yyyy-MM-dd");
            Map(x => x.AccountingPeriod).Name("Accounting period");
            Map(x => x.AverageNumberofFTEEmployees).Name("FTE");
        }
    }
}

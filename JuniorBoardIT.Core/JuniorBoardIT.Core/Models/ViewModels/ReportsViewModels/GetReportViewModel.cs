using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;

namespace JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels
{
    public class GetReportViewModel
    {
        public ReportsViewModel? ReportModel { get; set; }
        public JobOfferViewModel? JobOfferModel { get; set; }
    }
}

using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Cores.Models.Enums;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Queries
{
    public class GetReportsQuery : IQuery<GetReportsViewModel>
    {
        public ReportsTypeEnum ReportType { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

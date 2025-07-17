using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Queries
{
    public class GetReportQuery : IQuery<ReportsViewModel>
    {
        public Guid RGID { get; set; }
    }
}

using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Queries
{
    public class GetReportsQuery : IQuery<ReportsListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

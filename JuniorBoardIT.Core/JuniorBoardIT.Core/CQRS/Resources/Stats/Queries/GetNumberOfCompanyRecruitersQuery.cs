using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Queries
{
    public class GetNumberOfCompanyRecruitersQuery : IQuery<StatsBarChartViewModel>
    {
        public Guid CGID { get; set; }
    }
}

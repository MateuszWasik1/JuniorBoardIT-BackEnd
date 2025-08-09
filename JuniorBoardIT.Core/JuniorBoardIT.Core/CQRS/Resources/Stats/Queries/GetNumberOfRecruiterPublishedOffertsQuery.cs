using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Queries
{
    public class GetNumberOfRecruiterPublishedOffertsQuery : IQuery<StatsBarChartViewModel>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

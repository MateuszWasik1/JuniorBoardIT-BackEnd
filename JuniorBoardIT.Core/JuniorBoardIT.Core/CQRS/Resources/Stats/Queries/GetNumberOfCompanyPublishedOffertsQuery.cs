using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Queries
{
    public class GetNumberOfCompanyPublishedOffertsQuery : IQuery<StatsBarChartViewModel>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CGID { get; set; }
    }
}

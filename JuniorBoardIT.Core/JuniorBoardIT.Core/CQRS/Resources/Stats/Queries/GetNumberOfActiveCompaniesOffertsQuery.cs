using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Queries
{
    public class GetNumberOfActiveCompaniesOffertsQuery : IQuery<StatsBarChartViewModel>
    {
        public DateTime Date { get; set; }
    }
}

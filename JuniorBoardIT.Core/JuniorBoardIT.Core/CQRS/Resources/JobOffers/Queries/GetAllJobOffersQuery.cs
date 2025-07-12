using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries
{
    public class GetAllJobOffersQuery : IQuery<GetAllJobOffersViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

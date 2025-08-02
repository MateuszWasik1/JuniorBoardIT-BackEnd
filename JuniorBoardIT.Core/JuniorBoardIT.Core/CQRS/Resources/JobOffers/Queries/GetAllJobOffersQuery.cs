using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Models.Enums.JobOffers;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries
{
    public class GetAllJobOffersQuery : IQuery<GetAllJobOffersViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public EducationEnum Education { get; set; }
        public bool Favorites { get; set; }
    }
}

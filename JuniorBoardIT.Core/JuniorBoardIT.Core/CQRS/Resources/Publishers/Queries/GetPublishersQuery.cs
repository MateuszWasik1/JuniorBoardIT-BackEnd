using JuniorBoardIT.Core.Models.ViewModels.PublishersViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Publishers.Queries
{
    public class GetPublishersQuery : IQuery<PublishersListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

using JuniorBoardIT.Core.Models.ViewModels.AuthorsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Authors.Queries
{
    public class GetAuthorsQuery : IQuery<AuthorsListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

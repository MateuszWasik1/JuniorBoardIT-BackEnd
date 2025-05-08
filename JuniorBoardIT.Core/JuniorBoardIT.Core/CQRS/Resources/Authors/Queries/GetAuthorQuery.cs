using JuniorBoardIT.Core.Models.ViewModels.AuthorsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Authors.Queries
{
    public class GetAuthorQuery : IQuery<AuthorViewModel>
    {
        public Guid AGID { get; set; }
    }
}

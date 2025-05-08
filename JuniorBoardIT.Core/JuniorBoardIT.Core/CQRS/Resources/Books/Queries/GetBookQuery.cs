using JuniorBoardIT.Core.Models.ViewModels.BooksViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Books.Queries
{
    public class GetBookQuery : IQuery<BookViewModel>
    {
        public Guid BGID { get; set; }
    }
}

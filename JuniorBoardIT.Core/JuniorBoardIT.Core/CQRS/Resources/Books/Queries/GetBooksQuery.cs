using JuniorBoardIT.Core.Models.ViewModels.BooksViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Models.Enums;

namespace JuniorBoardIT.Core.CQRS.Resources.Books.Queries
{
    public class GetBooksQuery : IQuery<BooksListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public GenreEnum Genre { get; set; }
        public Guid AGID { get; set; }
        public Guid PGID { get; set; }
    }
}

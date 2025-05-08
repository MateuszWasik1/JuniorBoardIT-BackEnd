using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.BooksViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Books.Commands
{
    public class AddBookCommand : ICommand
    {
        public BookViewModel? Model { get; set; }
    }
}

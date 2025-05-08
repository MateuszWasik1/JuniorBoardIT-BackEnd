using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Books.Commands
{
    public class DeleteBookCommand : ICommand
    {
        public Guid BGID { get; set; }
    }
}

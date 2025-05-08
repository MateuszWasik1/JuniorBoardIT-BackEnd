using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.AuthorsViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Authors.Commands
{
    public class UpdateAuthorCommand : ICommand
    {
        public AuthorViewModel? Model { get; set; }
    }
}

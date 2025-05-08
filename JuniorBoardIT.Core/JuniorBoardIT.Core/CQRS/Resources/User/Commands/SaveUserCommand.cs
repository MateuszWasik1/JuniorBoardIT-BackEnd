using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Commands
{
    public class SaveUserCommand : ICommand
    {
        public UserViewModel? Model { get; set; }
    }
}

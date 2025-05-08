using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Commands
{
    public class SaveUserByAdminCommand : ICommand
    {
        public UserAdminViewModel? Model { get; set; }
    }
}

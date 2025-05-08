using JuniorBoardIT.Core.Models.ViewModels.AccountsViewModel;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Accounts.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public RegisterViewModel? Model { get; set; }
    }
}

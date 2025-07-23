using JuniorBoardIT.Core.Models.ViewModels.BugsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Commands
{
    public class ChangeBugStatusCommand : ICommand
    {
        public ChangeBugStatusViewModel? Model { get; set; }
    }
}

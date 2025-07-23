using JuniorBoardIT.Core.Models.ViewModels.BugsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Commands
{
    public class SaveBugCommand : ICommand
    {
        public BugViewModel? Model { get; set; }
    }
}

using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.ApplicationsViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Applications.Commands
{
    public class AddApplicationCommand : ICommand
    {
        public AddApplicationViewModel? Model { get; set; }
    }
}

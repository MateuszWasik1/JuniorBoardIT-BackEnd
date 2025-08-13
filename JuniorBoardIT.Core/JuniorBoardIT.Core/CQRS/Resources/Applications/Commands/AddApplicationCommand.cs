using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.ApplicationsViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands
{
    public class AddApplicationCommand : ICommand
    {
        public ApplicationViewModel? Model { get; set; }
    }
}

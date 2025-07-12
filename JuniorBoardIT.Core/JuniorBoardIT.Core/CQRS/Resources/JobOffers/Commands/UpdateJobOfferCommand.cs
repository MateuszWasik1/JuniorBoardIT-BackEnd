using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands
{
    public class UpdateJobOfferCommand : ICommand
    {
        public JobOfferViewModel? Model { get; set; }
    }
}

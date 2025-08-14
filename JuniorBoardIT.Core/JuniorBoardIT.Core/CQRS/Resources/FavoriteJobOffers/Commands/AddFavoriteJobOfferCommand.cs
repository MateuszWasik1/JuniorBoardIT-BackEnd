using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.FavoriteJobOffersViewModel;

namespace JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands
{
    public class AddFavoriteJobOfferCommand : ICommand
    {
        public AddFavoriteJobOfferViewModel Model { get; set; }
    }
}

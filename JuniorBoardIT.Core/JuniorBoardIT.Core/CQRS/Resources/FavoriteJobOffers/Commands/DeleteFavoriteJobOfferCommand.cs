using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands
{
    public class DeleteFavoriteJobOfferCommand : ICommand
    {
        public Guid FJOGID { get; set; }
    }
}

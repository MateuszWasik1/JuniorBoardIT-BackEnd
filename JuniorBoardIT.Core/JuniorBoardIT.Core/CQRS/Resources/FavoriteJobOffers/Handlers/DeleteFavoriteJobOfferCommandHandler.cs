using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands;
using JuniorBoardIT.Core.Exceptions.JobOffers;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class DeleteFavoriteJobOfferCommandHandler : ICommandHandler<DeleteFavoriteJobOfferCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteFavoriteJobOfferCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteFavoriteJobOfferCommand command)
        {
            var deletedFavoriteJobOffer = context.FavoriteJobOffers.FirstOrDefault(x => x.FJOGID == command.FJOGID);

            if (deletedFavoriteJobOffer == null)
                throw new JobOfferNotFoundExceptions("Nie znaleziono oferty pracy!");

            context.DeleteFavoriteJobOffer(deletedFavoriteJobOffer);
            context.SaveChanges();
        }
    }
}
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.Exceptions;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class DeleteJobOfferCommandHandler : ICommandHandler<DeleteJobOfferCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteJobOfferCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteJobOfferCommand command)
        {
            var deletedJobOffer = context.JobOffers.FirstOrDefault(x => x.JOGID == command.JOGID);

            if (deletedJobOffer == null)
                throw new UserNotFoundExceptions("Nie znaleziono oferty pracy!");

            context.DeleteJobOffer(deletedJobOffer);
            context.SaveChanges();
        }
    }
}
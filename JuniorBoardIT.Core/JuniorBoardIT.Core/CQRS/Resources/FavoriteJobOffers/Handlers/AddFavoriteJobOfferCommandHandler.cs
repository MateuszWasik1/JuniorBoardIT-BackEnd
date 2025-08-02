using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Handlers
{
    public class AddFavoriteJobOfferCommandHandler : ICommandHandler<AddFavoriteJobOfferCommand>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public AddFavoriteJobOfferCommandHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }
        public void Handle(AddFavoriteJobOfferCommand command)
        {

            var model = new Entities.FavoriteJobOffers()
            {
                FJOGID = Guid.NewGuid(),
                FJOUGID = Guid.Parse(user.UGID),
                FJOJOGID = command.JOGID
            };

            context.CreateOrUpdate(model);
            context.SaveChanges();
        }
    }
}

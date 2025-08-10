using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.User.Commands;
using JuniorBoardIT.Core.Exceptions;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Handlers
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteUserCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteUserCommand command)
        {
            var deletedUser = context.AllUsers.FirstOrDefault(x => x.UGID == command.UGID);

            if (deletedUser == null)
                throw new UserNotFoundExceptions("Nie znaleziono użytkownika!");

            var jobOffers = context.JobOffers.Where(x => x.JORGID == deletedUser.UGID).Any();

            if (jobOffers)
                throw new UserHasPublishedJobOffersExceptions("Użytkownik posiada opublikowane oferty pracy!");

            context.DeleteUser(deletedUser);
            context.SaveChanges();
        }
    }
}
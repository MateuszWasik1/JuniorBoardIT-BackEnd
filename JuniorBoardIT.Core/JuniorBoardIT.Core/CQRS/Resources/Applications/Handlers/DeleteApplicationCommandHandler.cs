using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.Exceptions.Accounts;
using JuniorBoardIT.Core.Exceptions.JobOffers;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class DeleteApplicationCommandHandler : ICommandHandler<DeleteApplicationCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteApplicationCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteApplicationCommand command)
        {
            var deletedApplication = context.Applications.FirstOrDefault(x => x.AGID == command.AGID);

            if (deletedApplication == null)
                throw new ApplicationNotFoundExceptions("Nie znaleziono aplikacji!");

            context.DeleteApplication(deletedApplication);
            context.SaveChanges();
        }
    }
}
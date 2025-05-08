using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Publishers.Commands;
using JuniorBoardIT.Core.Exceptions.Publishers;

namespace JuniorBoardIT.Core.CQRS.Resources.Publishers.Handlers
{
    public class DeletePublisherCommandHandler : ICommandHandler<DeletePublisherCommand>
    {
        private readonly IDataBaseContext context;
        public DeletePublisherCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeletePublisherCommand command)
        {
            var publisher = context.Publishers.FirstOrDefault(x => x.PGID == command.PGID);

            if (publisher == null)
                throw new PublisherNotFoundException("Nie udało się znaleźć wydawcy!");

            context.DeletePublisher(publisher);
            context.SaveChanges();
        }
    }
}

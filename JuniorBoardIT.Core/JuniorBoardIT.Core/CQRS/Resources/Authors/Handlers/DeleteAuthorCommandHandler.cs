using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Authors.Commands;
using JuniorBoardIT.Core.Exceptions.Authors;

namespace JuniorBoardIT.Core.CQRS.Resources.Authors.Handlers
{
    public class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteAuthorCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteAuthorCommand command)
        {
            var Author = context.Authors.FirstOrDefault(x => x.AGID == command.AGID);

            if (Author == null)
                throw new AuthorNotFoundException("Nie udało się znaleźć autora!");

            context.DeleteAuthor(Author);
            context.SaveChanges();
        }
    }
}

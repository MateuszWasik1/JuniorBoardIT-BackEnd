using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Commands;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Applications.Handlers
{
    public class AddApplicationCommandHandler : ICommandHandler<AddApplicationCommand>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public AddApplicationCommandHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }
        public void Handle(AddApplicationCommand command)
        {
            var model = new Entities.Applications()
            {
                AGID = Guid.NewGuid(),
                AUGID = Guid.Parse(user?.UGID), 
                AJOGID = command.Model.AJOGID,
                AApplicationDate = DateTime.Now,
            };

            context.CreateOrUpdate(model);
            context.SaveChanges();
        }
    }
}

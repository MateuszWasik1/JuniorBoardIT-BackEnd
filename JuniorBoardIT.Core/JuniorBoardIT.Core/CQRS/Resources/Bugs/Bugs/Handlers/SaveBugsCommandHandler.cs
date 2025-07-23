using JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Commands;
using JuniorBoardIT.Core.Exceptions.Bugs;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.Services;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Handlers
{
    public class SaveBugsCommandHandler : ICommandHandler<SaveBugCommand>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public SaveBugsCommandHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public void Handle(SaveBugCommand command)
        {
            if (command.Model.BTitle.Length == 0)
                throw new BugTitleRequiredExceptions("Tytuł błędu jest wymagany!");

            if (command.Model.BTitle.Length > 200)
                throw new BugTitleMax200Exceptions("Tytuł błędu nie może mieć więcej niż 200 znaków!");

            if (command.Model.BText.Length == 0)
                throw new BugTextRequiredExceptions("Tytuł błędu jest wymagany!");

            if (command.Model.BText.Length > 4000)
                throw new BugTextMax4000Exceptions("Tytuł błędu nie może mieć więcej niż 200 znaków!");

            var bug = new Entities.Bugs()
            {
                BGID = command.Model.BGID,
                BUID = user.UID,
                BDate = DateTime.Now,
                BTitle = command.Model.BTitle,
                BText = command.Model.BText,
                BStatus = command.Model.BStatus,
            };

            context.CreateOrUpdate(bug);
            context.SaveChanges();
        }
    }
}
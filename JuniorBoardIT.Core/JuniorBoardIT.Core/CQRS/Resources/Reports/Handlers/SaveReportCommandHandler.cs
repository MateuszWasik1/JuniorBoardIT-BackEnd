using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.Services;

namespace Organiser.Core.CQRS.Resources.Bugs.Bugs.Handlers
{
    public class SaveReportCommandHandler : ICommandHandler<SaveReportCommand>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public SaveReportCommandHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public void Handle(SaveReportCommand command)
        {
            //if (command.Model.BTitle.Length == 0)
            //    throw new BugTitleRequiredExceptions("Tytuł błędu jest wymagany!");

            //if (command.Model.BTitle.Length > 200)
            //    throw new BugTitleMax200Exceptions("Tytuł błędu nie może mieć więcej niż 200 znaków!");

            //if (command.Model.BText.Length == 0)
            //    throw new BugTextRequiredExceptions("Tytuł błędu jest wymagany!");

            //if (command.Model.BText.Length > 4000)
            //    throw new BugTextMax4000Exceptions("Tytuł błędu nie może mieć więcej niż 200 znaków!");

            //var bug = new Cores.Entities.Bugs()
            //{
            //    BGID = command.Model.BGID,
            //    BUID = user.UID,
            //    BDate = DateTime.Now,
            //    BTitle = command.Model.BTitle,
            //    BText = command.Model.BText,
            //    BStatus = command.Model.BStatus,
            //};

            //context.CreateOrUpdate(bug);
            //context.SaveChanges();
        }
    }
}
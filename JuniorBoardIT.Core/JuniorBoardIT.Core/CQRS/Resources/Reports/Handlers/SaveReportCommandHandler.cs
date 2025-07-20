using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers
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

            var report = new Core.Entities.Reports()
            {
                RGID = Guid.NewGuid(),
                RJOGID = command.Model.RJOGID,
                RReporterGID = Guid.Parse(user.UGID),
                RDate = DateTime.Now,
                RReasons = command.Model.RReasons,
                RText = command.Model.RText,
                RStatus = command.Model.RStatus,
            };

            context.CreateOrUpdate(report);
            context.SaveChanges();
        }
    }
}
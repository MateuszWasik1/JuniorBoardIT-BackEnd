using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.Models.Enums;
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

            var report = new Entities.Reports()
            {
                RGID = Guid.NewGuid(),
                RJOGID = command.Model.RJOGID,
                RReporterGID = Guid.Parse(user?.UGID),
                RDate = DateTime.Now,
                RReasons = command.Model.RReasons,
                RText = command.Model.RText,
                RStatus = command.Model.RStatus ?? ReportsStatusEnum.New,
            };

            context.CreateOrUpdate(report);
            context.SaveChanges();
        }
    }
}
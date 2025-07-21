using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.Exceptions;
using JuniorBoardIT.Core.Exceptions.Reports;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers
{
    public class ChangeReportStatusCommandHandler : ICommandHandler<ChangeReportStatusCommand>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public ChangeReportStatusCommandHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public void Handle(ChangeReportStatusCommand command)
        {
            var report = context.Reports.FirstOrDefault(x => x.RGID == command.Model.RGID);

            if (report == null)
                throw new ReportNotFoundExceptions("Nie znaleziono oferty pracy z zgłoszenia!");

            var currentUser = context.User.FirstOrDefault(x => x.UID == user.UID);

            if (currentUser == null)
                throw new UserNotFoundExceptions("Nie udało się odnaleźć użytkownika! Aktualizacja statusu zgłoszenia nie powiodła się.");

            if (currentUser.URID == (int) RoleEnum.Support || currentUser.URID == (int)RoleEnum.Admin)
            {
                if(report.RSupportGID == Guid.Empty || report.RSupportGID == null)
                    report.RSupportGID = currentUser.UGID;

                report.RStatus = command.Model.Status;

            } else {
                throw new Exception("Użytkownik nie posiada uprawnień administratora lub wsparcia.");
            }

            context.CreateOrUpdate(report);
            context.SaveChanges();
        }
    }
}
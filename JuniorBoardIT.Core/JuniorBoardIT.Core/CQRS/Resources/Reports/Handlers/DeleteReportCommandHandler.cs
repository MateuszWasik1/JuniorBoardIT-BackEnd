using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.Exceptions.Reports;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers
{
    public class DeleteReportCommandHandler : ICommandHandler<DeleteReportCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteReportCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteReportCommand command)
        {
            var Report = context.Reports.FirstOrDefault(x => x.RGID == command.RGID);

            if (Report == null)
                throw new ReportNotFoundException("Nie udało się znaleźć raportu!");

            context.DeleteReport(Report);
            context.SaveChanges();
        }
    }
}

using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Commands
{
    public class AddReportCommand : ICommand
    {
        public ReportViewModel? Model { get; set; }
    }
}

using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Commands
{
    public class ChangeReportStatusCommand : ICommand
    {
        public ChangeReportStatusViewModel? Model { get; set; }
    }
}

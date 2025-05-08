using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Commands
{
    public class DeleteReportCommand : ICommand
    {
        public Guid RGID { get; set; }
    }
}

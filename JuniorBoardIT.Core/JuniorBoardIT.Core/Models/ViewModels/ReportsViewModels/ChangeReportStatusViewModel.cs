using JuniorBoardIT.Core.Models.Enums;

namespace JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels
{
    public class ChangeReportStatusViewModel
    {
        public Guid BGID { get; set; }
        public ReportsStatusEnum Status { get; set; }
    }
}
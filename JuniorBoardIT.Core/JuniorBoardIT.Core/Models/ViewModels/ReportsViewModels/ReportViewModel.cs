using JuniorBoardIT.Core.Models.Enums;

namespace JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels
{
    public class ReportViewModel
    {
        public Guid RGID { get; set; }
        public string? RReasons { get; set; }
        public string? RText { get; set; }
        public ReportsStatusEnum? RStatus { get; set; }
    }
}
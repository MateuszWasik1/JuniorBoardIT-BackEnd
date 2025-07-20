using JuniorBoardIT.Core.Models.Enums;

namespace JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels
{
    public class ReportsViewModel
    {
        public int RID { get; set; }
        public Guid RGID { get; set; }
        public Guid RReporterGID { get; set; }
        public Guid? RSupportGID { get; set; }
        public DateTime RDate { get; set; }
        public string? RReasons { get; set; }
        public string? RText { get; set; }
        public ReportsStatusEnum? RStatus { get; set; }
    }
}
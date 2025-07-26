using JuniorBoardIT.Core.Models.Enums;

namespace JuniorBoardIT.Core.Models.ViewModels.BugsViewModels
{
    public class BugViewModel
    {
        public Guid BGID { get; set; }
        public string? BTitle { get; set; }
        public string? BText { get; set; }
        public BugStatusEnum? BStatus { get; set; }
    }
}
using JuniorBoardIT.Core.Models.Enums;

namespace JuniorBoardIT.Core.Models.ViewModels.BugsViewModels
{
    public class ChangeBugStatusViewModel
    {
        public Guid BGID { get; set; }
        public BugStatusEnum Status { get; set; }
    }
}
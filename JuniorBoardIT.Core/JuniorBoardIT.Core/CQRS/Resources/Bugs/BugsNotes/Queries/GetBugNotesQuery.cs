using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Models.ViewModels.BugsViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Bugs.BugsNotes.Queries
{
    public class GetBugNotesQuery : IQuery<GetBugsNotesViewModel>
    {
        public Guid BGID { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

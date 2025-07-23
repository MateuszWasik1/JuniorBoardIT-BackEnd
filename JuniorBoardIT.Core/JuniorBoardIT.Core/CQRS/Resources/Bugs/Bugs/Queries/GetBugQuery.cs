using JuniorBoardIT.Core.Models.ViewModels.BugsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Queries
{
    public class GetBugQuery : IQuery<BugViewModel>
    {
        public Guid BGID { get; set; }
    }
}

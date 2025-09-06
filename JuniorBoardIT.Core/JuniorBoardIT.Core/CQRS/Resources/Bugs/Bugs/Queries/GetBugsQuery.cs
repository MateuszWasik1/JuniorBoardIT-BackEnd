using JuniorBoardIT.Core.Models.ViewModels.BugsViewModels;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Queries
{
    public class GetBugsQuery : IQuery<GetBugsViewModel>
    {
        public BugTypeEnum BugType { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Message { get; set; }
    }
}

using JuniorBoardIT.Core.Models.ViewModels.ApplicationsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Applications.Queries
{
    public class GetApplicationsQuery : IQuery<ApplicationsViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public Guid UGID { get; set; }
    }
}

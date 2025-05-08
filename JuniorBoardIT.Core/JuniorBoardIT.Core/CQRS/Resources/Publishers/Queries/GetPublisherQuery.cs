using JuniorBoardIT.Core.Models.ViewModels.PublishersViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.Publishers.Queries
{
    public class GetPublisherQuery : IQuery<PublisherViewModel>
    {
        public Guid PGID { get; set; }
    }
}

using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Publishers.Queries;
using JuniorBoardIT.Core.Exceptions.Publishers;
using JuniorBoardIT.Core.Models.ViewModels.PublishersViewModels;
using Microsoft.EntityFrameworkCore;

namespace JuniorBoardIT.Core.CQRS.Resources.Publishers.Handlers
{
    public class GetPublisherQueryHandler : IQueryHandler<GetPublisherQuery, PublisherViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetPublisherQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public PublisherViewModel Handle(GetPublisherQuery query)
        {
            var publisher = context.Publishers.AsNoTracking().FirstOrDefault(x => x.PGID == query.PGID);

            if (publisher == null)
                throw new PublisherNotFoundException("Nie udało się znaleźć wydawcy!");

            var publisherViewModel = mapper.Map<JuniorBoardIT.Core.Entities.Publishers, PublisherViewModel>(publisher);

            return publisherViewModel;
        }
    }
}

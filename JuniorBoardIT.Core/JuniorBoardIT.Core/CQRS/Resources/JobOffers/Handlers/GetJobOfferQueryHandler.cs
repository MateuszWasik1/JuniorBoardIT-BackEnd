using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Handlers
{
    public class GetJobOfferQueryHandler : IQueryHandler<GetJobOfferQuery, JobOfferViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetJobOfferQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public JobOfferViewModel Handle(GetJobOfferQuery query)
        {
            var jobOffer = context.JobOffers.FirstOrDefault(x => x.JOGID == query.JOGID);

            if (jobOffer == null)
                throw new JobOfferNotFoundExceptions("Nie znaleziono oferty pracy!");

            var model = mapper.Map<Entities.JobOffers, JobOfferViewModel>(jobOffer);

            return model;
        }
    }
}

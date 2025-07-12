using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Handlers
{
    public class GetJobOfferQueryHandler : IQueryHandler<GetJobOfferQuery, JobOfferViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        private readonly IMapper mapper;
        public GetJobOfferQueryHandler(IDataBaseContext context, IUserContext user, IMapper mapper)
        {
            this.context = context;
            this.user = user;
            this.mapper = mapper;
        }

        public JobOfferViewModel Handle(GetJobOfferQuery query)
        {
            var jobOffer = context.JobOffers.FirstOrDefault(x => x.JOGID == query.JOGID);

            if (jobOffer == null)
                throw new JobOfferNotFoundExceptions("Nie znaleziono użytkownika!");

            var model = mapper.Map<Entities.JobOffers, JobOfferViewModel>(jobOffer);

            return model;
        }
    }
}

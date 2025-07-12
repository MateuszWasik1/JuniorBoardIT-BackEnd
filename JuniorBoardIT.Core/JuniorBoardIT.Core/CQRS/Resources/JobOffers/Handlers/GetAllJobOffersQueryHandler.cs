using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class GetAllJobOffersQueryHandler : IQueryHandler<GetAllJobOffersQuery, GetAllJobOffersViewModel>
    {

        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetAllJobOffersQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetAllJobOffersViewModel Handle(GetAllJobOffersQuery query)
        {
            var jobOffers = context.JobOffers.ToList();

            var allJobOffersViewModel = new List<JobOfferViewModel>();

            var count = jobOffers.Count;
            jobOffers = jobOffers.Skip(query.Skip).Take(query.Take).ToList();

            jobOffers.ForEach(x =>
            {
                var model = mapper.Map<Entities.JobOffers, JobOfferViewModel>(x);
                allJobOffersViewModel.Add(model);
            });

            var model = new GetAllJobOffersViewModel()
            {
                List = allJobOffersViewModel,
                Count = count,
            };

            return model;
        }
    }
}

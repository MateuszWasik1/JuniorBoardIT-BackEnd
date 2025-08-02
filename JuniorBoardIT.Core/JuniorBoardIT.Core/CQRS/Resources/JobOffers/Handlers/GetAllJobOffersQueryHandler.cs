using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Models.Enums.JobOffers;
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
            var jobOffers = new List<Entities.JobOffers>();

            if(query.Education == EducationEnum.Elementary)
                jobOffers = context.JobOffers.Where(x => x.JOEducation == EducationEnum.Elementary).ToList();
            else if(query.Education == EducationEnum.Secondary)
                jobOffers = context.JobOffers.Where(x => x.JOEducation <= EducationEnum.Secondary).ToList();
            else if (query.Education == EducationEnum.Vocational)
                jobOffers = context.JobOffers.Where(x => x.JOEducation == EducationEnum.Elementary || x.JOEducation == EducationEnum.Vocational).ToList();
            else if (query.Education == EducationEnum.HigherILevel)
                jobOffers = context.JobOffers.Where(x => x.JOEducation <= EducationEnum.HigherILevel).ToList();
            else if (query.Education == EducationEnum.HigherIILevel)
                jobOffers = context.JobOffers.Where(x => x.JOEducation <= EducationEnum.HigherIILevel).ToList();
            else if (query.Education == EducationEnum.All)
                jobOffers = context.JobOffers.ToList();
            else
                jobOffers = context.JobOffers.ToList();

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

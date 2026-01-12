using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class GetJobOfferQueryHandler : IQueryHandler<GetJobOfferQuery, GetJobOfferViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetJobOfferQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetJobOfferViewModel Handle(GetJobOfferQuery query)
        {
            var jobOffer = context.JobOffers.FirstOrDefault(x => x.JOGID == query.JOGID);

            if (jobOffer == null)
                throw new JobOfferNotFoundExceptions("Nie znaleziono oferty pracy!");

            var jobOfferModel = mapper.Map<Entities.JobOffers, JobOfferViewModel>(jobOffer);

            var company = context.Companies.FirstOrDefault(x => x.CGID == jobOfferModel.JOCGID) ?? new Entities.Companies();

            var companyModel = mapper.Map<Entities.Companies, CompanyViewModel>(company);

            var model = new GetJobOfferViewModel()
            {
                JobOffer = jobOfferModel,
                Company = companyModel,
            };

            return model;
        }
    }
}

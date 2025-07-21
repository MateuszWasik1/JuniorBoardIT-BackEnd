using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using JuniorBoardIT.Core.Exceptions.Reports;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers
{
    public class GetReportQueryHandler : IQueryHandler<GetReportQuery, GetReportViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetReportQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetReportViewModel Handle(GetReportQuery query)
        {
            var report = context.Reports.FirstOrDefault(x => x.RGID ==  query.RGID);

            if (report == null)
                throw new ReportNotFoundExceptions("Nie znaleziono wskazanego zgłoszenia!");

            var reportViewModel = mapper.Map<Entities.Reports, ReportsViewModel>(report);

            var jobOffer = context.JobOffers.FirstOrDefault(x => x.JOGID == report.RJOGID);

            if (jobOffer == null)
                throw new JobOfferNotFoundExceptions("Nie znaleziono wskazanej oferty pracy ze zgłoszenia!");

            var jobOfferViewModel = mapper.Map<Entities.JobOffers, JobOfferViewModel>(jobOffer);

            var model = new GetReportViewModel()
            {
                ReportModel = reportViewModel,
                JobOfferModel = jobOfferViewModel
            };

            return model;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Exceptions.Companies;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Handlers
{
    public class GetNumberOfActiveCompaniesOffertsQueryHandler : IQueryHandler<GetNumberOfActiveCompaniesOffertsQuery, StatsBarChartViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public GetNumberOfActiveCompaniesOffertsQueryHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public StatsBarChartViewModel Handle(GetNumberOfActiveCompaniesOffertsQuery query)
        {
            Guid CGID;

            if (query.CGID == Guid.Empty)
                CGID = context.User.FirstOrDefault(x => x.UGID == Guid.Parse(user.UGID))?.UCompanyGID ?? Guid.Empty;
            else
                CGID = query.CGID;

            var company = context.Companies.FirstOrDefault(x => CGID == x.CGID);

            if (company == null)
                throw new CompanyNotFoundExceptions("Nie znaleziono firmy!");

            var jobOffers = context.JobOffers.Where(x => x.JORGID == CGID && query.Date.Date == x.JOPostedAt.Value.Date).AsNoTracking().Count();

            var data = new StatsBarChartViewModel()
            {
                Labels = new List<string>(),
                Datasets = new ChartDatasetViewModel(),
            };

            var model = new ChartDatasetViewModel()
            {
                Label = $"Aktywne oferty firmy {company.CName}",
                Data = new List<decimal>(),
            };

            model?.Data?.Add(jobOffers);

            data?.Labels?.Add($"{new DateTime(query.Date.Year)}-{new DateTime(query.Date.Month)}-{new DateTime(query.Date.Day)}");

            data.Datasets = model;

            return data;
        }
    }
}

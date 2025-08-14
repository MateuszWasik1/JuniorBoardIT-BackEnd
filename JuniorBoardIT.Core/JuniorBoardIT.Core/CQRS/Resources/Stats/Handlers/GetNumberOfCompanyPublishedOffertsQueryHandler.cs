using Microsoft.EntityFrameworkCore;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.Models.Helpers;
using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Exceptions.Companies;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Handlers
{
    public class GetNumberOfCompanyPublishedOffertsQueryHandler : IQueryHandler<GetNumberOfCompanyPublishedOffertsQuery, StatsBarChartViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;

        public GetNumberOfCompanyPublishedOffertsQueryHandler(IDataBaseContext context, IUserContext user)
        { 
            this.context = context; 
            this.user = user; 
        }

        public StatsBarChartViewModel Handle(GetNumberOfCompanyPublishedOffertsQuery query)
        {
            Guid CGID;

            if (query.CGID == Guid.Empty)
                CGID = context.User.FirstOrDefault(x => x.UGID == Guid.Parse(user.UGID))?.UCompanyGID ?? Guid.Empty;
            else
                CGID = query.CGID;

            var company = context.Companies.FirstOrDefault(x => CGID == x.CGID);

            if (company == null)
                throw new CompanyNotFoundExceptions("Nie znaleziono firmy!");

            var jobOffers = context.JobOffers.Where(x => x.JORGID == CGID && query.StartDate <= x.JOPostedAt && x.JOPostedAt <= query.EndDate).AsNoTracking().ToList();
            //ToDo poprawić JORGID na JOCGID po dodaniu firm do ofert pracy

            var timeSpanBetweenStartAndEndDate = MonthsBetweenDatesHelper.MonthsBetween(query.StartDate, query.EndDate);

            var data = new StatsBarChartViewModel()
            {
                Labels = new List<string>(),
                Datasets = new ChartDatasetViewModel(),
            };

            var model = new ChartDatasetViewModel()
            {
                Label = $"Liczba ofert pracy opublikowanych przez firmę {company.CName}",
                Data = new List<decimal>(),
            };

            foreach (var x in timeSpanBetweenStartAndEndDate)
            {
                var month = new DateTime(x.Year, x.Month, 1);
                var nextMonth = month.AddMonths(1).AddSeconds(-1);

                if (query.StartDate.Year == x.Year && query.StartDate.Month == x.Month)
                {
                    month = new DateTime(x.Year, x.Month, query.StartDate.Day);
                    nextMonth = month.AddMonths(1).AddDays(-query.StartDate.Day + 1).AddSeconds(-1);
                }
                if (query.EndDate.Year == x.Year && query.EndDate.Month == x.Month)
                    nextMonth = new DateTime(x.Year, x.Month, query.EndDate.Day, 23, 59, 59);

                var jobOffersForMonth = jobOffers.Where(x => month <= x.JOPostedAt && x.JOPostedAt <= nextMonth).Count();

                model?.Data?.Add(jobOffersForMonth);

                data?.Labels?.Add($"{x.Year}-{x.Month}");
            }

            data.Datasets = model;

            return data;
        }
    }
}

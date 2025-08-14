using Microsoft.EntityFrameworkCore;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.Models.Helpers;
using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Handlers
{
    public class GetNumberOfCompaniesPublishedOffertsQueryHandler : IQueryHandler<GetNumberOfCompaniesPublishedOffertsQuery, StatsBarChartViewModel>
    {
        private readonly IDataBaseContext context;
        public GetNumberOfCompaniesPublishedOffertsQueryHandler(IDataBaseContext context) => this.context = context;

        public StatsBarChartViewModel Handle(GetNumberOfCompaniesPublishedOffertsQuery query)
        {
            var jobOffersForPeriod = context.JobOffers
                .Where(x => query.StartDate <= x.JOPostedAt && x.JOPostedAt <= query.EndDate)
                .OrderBy(x => x.JOCreatedAt)
                .AsNoTracking()
                .ToList();

            var timeSpanBetweenStartAndEndDate = MonthsBetweenDatesHelper.MonthsBetween(query.StartDate, query.EndDate);

            var data = new StatsBarChartViewModel()
            {
                Labels = new List<string>(),
                Datasets = new ChartDatasetViewModel(),
            };

            var model = new ChartDatasetViewModel()
            {
                Label = $"Opublikowane oferty pracy przez wszystkie firmy.",
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

                var postedOffers = jobOffersForPeriod.Where(x => month <= x.JOPostedAt && x.JOPostedAt <= nextMonth).Count();

                model?.Data?.Add(postedOffers);

                data?.Labels?.Add($"{x.Year}-{x.Month}");
            }

            data.Datasets = model;

            return data;
        }
    }
}

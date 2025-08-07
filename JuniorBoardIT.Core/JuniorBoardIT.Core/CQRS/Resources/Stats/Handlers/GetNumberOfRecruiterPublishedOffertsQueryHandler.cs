using Microsoft.EntityFrameworkCore;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.Models.Helpers;
using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Handlers
{
    public class GetNumberOfRecruiterPublishedOffertsQueryHandler : IQueryHandler<GetNumberOfRecruiterPublishedOffertsQuery, StatsBarChartViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public GetNumberOfRecruiterPublishedOffertsQueryHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public StatsBarChartViewModel Handle(GetNumberOfRecruiterPublishedOffertsQuery query)
        {
            var jobOffersForPeriod = context.JobOffers
                .Where(x => x.JORGID == Guid.Parse(user.UGID) && query.StartDate <= x.JOPostedAt && x.JOPostedAt <= query.EndDate)
                .OrderBy(x => x.JOPostedAt)
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
                Label = $"Oferty Pracy",
                Data = new List<decimal>(),
            };

            foreach (var timeSpan in timeSpanBetweenStartAndEndDate)
            {
                var month = new DateTime(timeSpan.Year, timeSpan.Month, 1);
                var nextMonth = month.AddMonths(1).AddSeconds(-1);

                if (query.StartDate.Year == timeSpan.Year && query.StartDate.Month == timeSpan.Month)
                {
                    month = new DateTime(timeSpan.Year, timeSpan.Month, query.StartDate.Day);
                    nextMonth = month.AddMonths(1).AddDays(-query.StartDate.Day + 1).AddSeconds(-1);
                }
                if (query.EndDate.Year == timeSpan.Year && query.EndDate.Month == timeSpan.Month)
                    nextMonth = new DateTime(timeSpan.Year, timeSpan.Month, query.EndDate.Day, 23, 59, 59);

                var jobOffersForMonth = jobOffersForPeriod.Where(x => month <= x.JOPostedAt && x.JOPostedAt <= nextMonth).Count();

                model?.Data?.Add(jobOffersForMonth);

                data?.Labels?.Add($"{timeSpan.Year}-{timeSpan.Month}");
            }

            data.Datasets = model;

            return data;
        }
    }
}

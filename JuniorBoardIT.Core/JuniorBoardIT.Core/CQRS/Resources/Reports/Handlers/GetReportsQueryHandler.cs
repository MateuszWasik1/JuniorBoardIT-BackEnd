using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using Microsoft.EntityFrameworkCore;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers
{
    public class GetReportsQueryHandler : IQueryHandler<GetReportsQuery, ReportsListViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetReportsQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ReportsListViewModel Handle(GetReportsQuery query)
        {
            var reports = context.Reports.AsNoTracking().ToList();
            var reportsViewModel = new List<ReportsViewModel>();

            var count = reports.Count;
            reports = reports.Skip(query.Skip).Take(query.Take).ToList();

            reports.ForEach(x =>
            {
                var pVM = mapper.Map<Entities.Reports, ReportsViewModel>(x);

                reportsViewModel.Add(pVM);
            });

            var model = new ReportsListViewModel()
            {
                List = reportsViewModel,
                Count = count
            };

            return model;
        }
    }
}

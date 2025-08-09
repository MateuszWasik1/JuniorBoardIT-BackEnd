using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers
{
    public class GetReportsQueryHandler : IQueryHandler<GetReportsQuery, GetReportsViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        private readonly IMapper mapper;
        public GetReportsQueryHandler(IDataBaseContext context, IUserContext user, IMapper mapper)
        {
            this.context = context;
            this.user = user;
            this.mapper = mapper;
        }

        public GetReportsViewModel Handle(GetReportsQuery query)
        {
            var reports = new List<Entities.Reports>();
            var reportsViewModel = new List<ReportsViewModel>();
            var currentUserRole = context.User.AsNoTracking().FirstOrDefault(x => x.UID == user.UID)?.URID ?? (int)RoleEnum.User;

            var count = 0;

            if (query.ReportType == ReportsTypeEnum.New)
                reports = context.Reports.Where(x => x.RSupportGID == Guid.Empty || x.RSupportGID == null || x.RStatus == ReportsStatusEnum.New).OrderBy(x => x.RDate).AsNoTracking().ToList();

            else if (query.ReportType == ReportsTypeEnum.ImVerificator)
                reports = context.Reports.Where(x => x.RSupportGID == Guid.Parse(user.UGID)).OrderBy(x => x.RDate).AsNoTracking().ToList();

            else if (query.ReportType == ReportsTypeEnum.All && currentUserRole == (int) RoleEnum.Admin)
                reports = context.Reports.OrderBy(x => x.RDate).AsNoTracking().ToList();

            else if (query.ReportType == ReportsTypeEnum.All && currentUserRole != (int) RoleEnum.Admin)
                reports = context.Reports.Where(x => x.RSupportGID == Guid.Parse(user.UGID) || x.RSupportGID == Guid.Empty).OrderBy(x => x.RDate).AsNoTracking().ToList();

            count = reports.Count;
            reports = reports.Skip(query.Skip).Take(query.Take).ToList();

            reports.ForEach(x =>
            {
                var rVM = mapper.Map<Entities.Reports, ReportsViewModel>(x);

                reportsViewModel.Add(rVM);
            });

            var model = new GetReportsViewModel()
            {
                List = reportsViewModel,
                Count = count
            };

            return model;
        }
    }
}
using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers
{
    public class GetReportQueryHandler : IQueryHandler<GetReportQuery, ReportsViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        private readonly IMapper mapper;
        public GetReportQueryHandler(IDataBaseContext context, IUserContext user, IMapper mapper)
        {
            this.context = context;
            this.user = user;
            this.mapper = mapper;
        }

        public ReportsViewModel Handle(GetReportQuery query)
        {
            //var bug = new Cores.Entities.Bugs();
            //var currentUserRole = context.User.AsNoTracking().FirstOrDefault(x => x.UID == user.UID)?.URID ?? (int) RoleEnum.User;

            //if (currentUserRole == (int) RoleEnum.Admin || currentUserRole == (int) RoleEnum.Support)
            //    bug = context.AllBugs.AsNoTracking().FirstOrDefault(x => x.BGID == query.BGID);
            //else
            //    bug = context.Bugs.AsNoTracking().FirstOrDefault(x => x.BGID == query.BGID);

            //if (bug == null)
            //    throw new BugNotFoundExceptions("Nie znaleziono wskazanego błędu!");

            //var bugViewModel = mapper.Map<Cores.Entities.Bugs, BugViewModel>(bug);

            //return bugViewModel;

            return new ReportsViewModel();
        }
    }
}

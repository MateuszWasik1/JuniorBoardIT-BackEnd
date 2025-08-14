using Microsoft.EntityFrameworkCore;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Services;
using JuniorBoardIT.Core.Exceptions.Companies;

namespace JuniorBoardIT.Core.CQRS.Resources.Stats.Handlers
{
    public class GetNumberOfCompanyRecruitersQueryHandler : IQueryHandler<GetNumberOfCompanyRecruitersQuery, StatsBarChartViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public GetNumberOfCompanyRecruitersQueryHandler(IDataBaseContext context, IUserContext user) 
        {
            this.context = context;
            this.user = user;
        }

        public StatsBarChartViewModel Handle(GetNumberOfCompanyRecruitersQuery query)
        {
            Guid CGID;

            if (query.CGID == Guid.Empty)
                CGID = context.User.FirstOrDefault(x => x.UGID == Guid.Parse(user.UGID))?.UCompanyGID ?? Guid.Empty;
            else
                CGID = query.CGID;

            var company = context.Companies.FirstOrDefault(x => CGID == x.CGID);

            if (company == null)
                throw new CompanyNotFoundExceptions("Nie znaleziono firmy!");

            var recruiters = context.AllUsers.Where(x => x.UCompanyGID == company.CGID).AsNoTracking().Count();

            var data = new StatsBarChartViewModel()
            {
                Labels = new List<string>(),
                Datasets = new ChartDatasetViewModel(),
            };

            var model = new ChartDatasetViewModel()
            {
                Label = $"Rekruterzy firmy {company.CName}",
                Data = new List<decimal>(),
            };

            model?.Data?.Add(recruiters);

            data?.Labels?.Add($"{DateTime.Now.Date}");

            data.Datasets = model;

            return data;
        }
    }
}

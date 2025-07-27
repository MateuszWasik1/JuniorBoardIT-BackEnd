using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Queries;
using JuniorBoardIT.Core.Exceptions.Companies;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Handlers
{
    public class GetCompanyQueryHandler : IQueryHandler<GetCompanyQuery, CompanyViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetCompanyQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public CompanyViewModel Handle(GetCompanyQuery query)
        {
            var company = context.Companies.FirstOrDefault(x => x.CGID == query.CGID);

            if (company == null)
                throw new CompanyNotFoundExceptions("Nie znaleziono firmy!");

            var model = mapper.Map<Entities.Companies, CompanyViewModel>(company);

            return model;
        }
    }
}

using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Queries;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers
{
    public class GetCompaniesQueryHandler : IQueryHandler<GetCompaniesQuery, GetCompaniesViewModel>
    {

        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetCompaniesQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetCompaniesViewModel Handle(GetCompaniesQuery query)
        {
            var companies = context.Companies.ToList();

            var compeniesViewModel = new List<CompanyViewModel>();

            var count = companies.Count;

            if (query.Name != null && query.Name != "")
            {
                companies = companies
                    .Where(company => company.CName.Contains(query.Name) || 
                        company.CEmail.Contains(query.Name) || 
                        company.CNIP.Contains(query.Name) || 
                        company.CKRS.Contains(query.Name) || 
                        company.CRegon.Contains(query.Name))
                    .ToList();
            }

            companies = companies.Skip(query.Skip).Take(query.Take).ToList();

            companies.ForEach(x =>
            {
                var model = mapper.Map<Entities.Companies, CompanyViewModel>(x);
                compeniesViewModel.Add(model);
            });

            var model = new GetCompaniesViewModel()
            {
                List = compeniesViewModel,
                Count = count,
            };

            return model;
        }
    }
}

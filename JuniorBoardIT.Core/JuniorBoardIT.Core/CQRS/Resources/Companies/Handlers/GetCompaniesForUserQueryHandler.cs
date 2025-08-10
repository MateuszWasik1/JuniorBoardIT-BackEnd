using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Queries;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using Microsoft.EntityFrameworkCore;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers
{
    public class GetCompaniesForUserQueryHandler : IQueryHandler<GetCompaniesForUserQuery, GetCompaniesForUserViewModel>
    {

        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetCompaniesForUserQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetCompaniesForUserViewModel Handle(GetCompaniesForUserQuery query)
        {
            var companies = context.Companies.AsNoTracking().ToList();

            var compeniesViewModel = new List<GetCompanyForUserViewModel>();

            companies.ForEach(x =>
            {
                var model = mapper.Map<Entities.Companies, GetCompanyForUserViewModel>(x);
                compeniesViewModel.Add(model);
            });

            var model = new GetCompaniesForUserViewModel()
            {
                List = compeniesViewModel,
            };

            return model;
        }
    }
}

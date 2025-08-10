using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.User.Queries;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using Microsoft.EntityFrameworkCore;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Handlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, GetUsersAdminViewModel>
    {

        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetAllUsersQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetUsersAdminViewModel Handle(GetAllUsersQuery query)
        {
            var usersData = context.AllUsers.ToList();

            var usersAdmViewModel = new List<UsersAdminViewModel>();

            var count = usersData.Count;
            usersData = usersData.Skip(query.Skip).Take(query.Take).ToList();

            var companies = context.Companies.AsNoTracking().ToList();
            var companiesViewModel = new List<GetCompanyForUserViewModel>();

            companies.ForEach(company =>
            {
                var model = mapper.Map<Entities.Companies, GetCompanyForUserViewModel>(company);
                companiesViewModel.Add(model);
            });

            usersData.ForEach(x => {
                var model = mapper.Map<Entities.User, UsersAdminViewModel>(x);

                if(x.UCompanyGID != Guid.Empty && model.UCompanyGID != null)
                {
                    model.UCompany = companiesViewModel.FirstOrDefault(x => x.CGID == model.UCompanyGID).CName;
                }

                usersAdmViewModel.Add(model);
            });


            var model = new GetUsersAdminViewModel()
            {
                List = usersAdmViewModel,
                Count = count,
            };

            return model;
        }
    }
}

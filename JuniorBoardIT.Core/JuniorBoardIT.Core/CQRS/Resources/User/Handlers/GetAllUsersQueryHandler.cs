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
            var usersData = context.AllUsers.AsNoTracking().ToList();

            var usersAdmViewModel = new List<UsersAdminViewModel>();

            var count = usersData.Count;

            if (query.Role != 0)
            {
                usersData = usersData.Where(user => user.URID == query.Role).ToList();
            }

            if (query.Name != null && query.Name != "")
            {
                usersData = usersData
                    .Where(user => user.UUserName.Contains(query.Name) || 
                        user.UFirstName.Contains(query.Name) || 
                        user.ULastName.Contains(query.Name) || 
                        user.UEmail.Contains(query.Name))
                    .ToList();
            }

            if(query.HasCompany) 
            {
                usersData = usersData.Where(user => user.UCompanyGID != null && user.UCompanyGID != Guid.Empty).ToList();
            }

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

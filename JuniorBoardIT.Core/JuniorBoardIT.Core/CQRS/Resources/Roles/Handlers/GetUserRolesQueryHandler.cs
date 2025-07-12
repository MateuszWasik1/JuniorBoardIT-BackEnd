using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Queries;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Models.ViewModels;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Roles.Handlers
{
    public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, RolesViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public GetUserRolesQueryHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public RolesViewModel Handle(GetUserRolesQuery query) 
        {
            var userRole = context.User.FirstOrDefault(x => x.UID == user.UID)?.URID ?? (int) RoleEnum.User;

            var model = new RolesViewModel()
            {
                IsAdmin = userRole == (int) RoleEnum.Admin,
                IsPremium = userRole == (int) RoleEnum.Premium,
                IsRecruiter = userRole == (int) RoleEnum.Recruiter || userRole == (int)RoleEnum.Support || userRole == (int)RoleEnum.Admin,
                IsSupport = userRole == (int) RoleEnum.Admin || userRole == (int) RoleEnum.Support,
                IsUser = userRole == (int) RoleEnum.Admin || userRole == (int) RoleEnum.Support || userRole == (int) RoleEnum.User,
            };

            return model;
        }
    }
}

using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Queries;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.Roles.Handlers
{
    public class GetIsUserAdminQueryHandler : IQueryHandler<GetIsUserAdminQuery, bool>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public GetIsUserAdminQueryHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public bool Handle(GetIsUserAdminQuery query) => context.User.FirstOrDefault(x => x.UID == user.UID)?.URID == (int) RoleEnum.Admin;
    }
}

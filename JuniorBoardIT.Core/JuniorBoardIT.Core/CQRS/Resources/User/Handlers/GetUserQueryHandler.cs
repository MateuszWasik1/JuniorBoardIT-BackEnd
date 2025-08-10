using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.User.Queries;
using JuniorBoardIT.Core.Exceptions;
using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Handlers
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        private readonly IMapper mapper;
        public GetUserQueryHandler(IDataBaseContext context, IUserContext user, IMapper mapper)
        {
            this.context = context;
            this.user = user;
            this.mapper = mapper;
        }

        public UserViewModel Handle(GetUserQuery query)
        {
            var userData = context.User.FirstOrDefault(x => x.UID == user.UID);

            if (userData == null)
                throw new UserNotFoundExceptions("Nie znaleziono użytkownika!");

            var model = mapper.Map<Entities.User, UserViewModel>(userData);

            if (model.UCompanyGID != Guid.Empty)
            {
                model.UCompany = context.Companies.FirstOrDefault(x => x.CGID == model.UCompanyGID).CName;
            }

            return model;
        }
    }
}

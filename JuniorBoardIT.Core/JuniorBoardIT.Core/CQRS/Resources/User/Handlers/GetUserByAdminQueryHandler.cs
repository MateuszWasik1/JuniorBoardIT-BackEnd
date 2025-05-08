using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.User.Queries;
using JuniorBoardIT.Core.Exceptions;
using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Handlers
{
    public class GetUserByAdminQueryHandler : IQueryHandler<GetUserByAdminQuery, UserAdminViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetUserByAdminQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public UserAdminViewModel Handle(GetUserByAdminQuery query)
        {
            var userData = context.AllUsers.FirstOrDefault(x => x.UGID == query.UGID);

            if (userData == null)
                throw new UserNotFoundExceptions("Nie znaleziono użytkownika!");

            var model = mapper.Map<Entities.User, UserAdminViewModel>(userData);

            return model;
        }
    }
}
using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Queries
{
    public class GetUserQuery : IQuery<UserViewModel>
    {
    }
}

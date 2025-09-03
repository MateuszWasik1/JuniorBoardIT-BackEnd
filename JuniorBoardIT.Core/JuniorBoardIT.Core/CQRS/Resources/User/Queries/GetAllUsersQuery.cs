using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Queries
{
    public class GetAllUsersQuery : IQuery<GetUsersAdminViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string? Name { get; set; }
        public bool HasCompany { get; set; }
        public int Role { get; set; }
    }
}

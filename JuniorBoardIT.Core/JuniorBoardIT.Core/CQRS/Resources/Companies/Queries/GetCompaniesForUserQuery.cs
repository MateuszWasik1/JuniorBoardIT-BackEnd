using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Queries
{
    public class GetCompaniesForUserQuery : IQuery<GetCompaniesForUserViewModel>
    {
    }
}

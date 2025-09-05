using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Queries
{
    public class GetCompaniesQuery : IQuery<GetCompaniesViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string? Name { get; set; }
    }
}

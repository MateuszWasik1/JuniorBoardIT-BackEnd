using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Commands
{
    public class UpdateCompanyCommand : ICommand
    {
        public CompanyViewModel? Model { get; set; }
    }
}

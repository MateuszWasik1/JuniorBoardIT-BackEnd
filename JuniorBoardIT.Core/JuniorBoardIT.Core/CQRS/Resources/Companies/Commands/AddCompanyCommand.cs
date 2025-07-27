using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Commands
{
    public class AddCompanyCommand : ICommand
    {
        public AddCompanyViewModel? Model { get; set; }
    }
}

using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Commands
{
    public class DeleteCompanyCommand : ICommand
    {
        public Guid CGID { get; set; }
    }
}

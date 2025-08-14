using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Applications.Commands
{
    public class AddApplicationCommand : ICommand
    {
        public Guid AJOGID { get; set; }
    }
}

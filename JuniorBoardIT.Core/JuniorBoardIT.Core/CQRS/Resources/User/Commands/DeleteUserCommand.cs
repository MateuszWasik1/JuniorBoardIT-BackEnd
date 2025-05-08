using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public Guid UGID { get; set; }
    }
}

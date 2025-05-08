using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Authors.Commands
{
    public class DeleteAuthorCommand : ICommand
    {
        public Guid AGID { get; set; }
    }
}

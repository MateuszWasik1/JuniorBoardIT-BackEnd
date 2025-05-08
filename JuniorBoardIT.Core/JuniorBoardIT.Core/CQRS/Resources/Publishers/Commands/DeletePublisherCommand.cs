using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Publishers.Commands
{
    public class DeletePublisherCommand : ICommand
    {
        public Guid PGID { get; set; }
    }
}

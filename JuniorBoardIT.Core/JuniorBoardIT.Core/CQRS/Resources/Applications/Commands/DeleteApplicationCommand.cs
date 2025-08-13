using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands
{
    public class DeleteApplicationCommand : ICommand
    {
        public Guid AGID { get; set; }
    }
}

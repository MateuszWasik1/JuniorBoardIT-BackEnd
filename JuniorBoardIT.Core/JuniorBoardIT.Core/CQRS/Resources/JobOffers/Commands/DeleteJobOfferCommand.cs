using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands
{
    public class DeleteJobOfferCommand : ICommand
    {
        public Guid JOGID { get; set; }
    }
}

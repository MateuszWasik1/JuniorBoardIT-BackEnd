using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.Models.ViewModels.PublishersViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.Publishers.Commands
{
    public class UpdatePublisherCommand : ICommand
    {
        public PublisherViewModel? Model { get; set; }
    }
}

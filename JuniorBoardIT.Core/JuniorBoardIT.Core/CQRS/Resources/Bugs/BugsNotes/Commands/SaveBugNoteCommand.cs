using JuniorBoardIT.Core.Models.ViewModels.BugsViewModels;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;

namespace JuniorBoardIT.Core.CQRS.Resources.Bugs.BugsNotes.Commands
{
    public class SaveBugNoteCommand : ICommand
    { 
        public BugsNotesViewModel? Model { get; set; }
    }
}

using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Interfaces.Validators
{
    public interface INoteValidationToDoApp
    {
        public bool IsNoteValid(NoteDTO noteDTO);
    }
}

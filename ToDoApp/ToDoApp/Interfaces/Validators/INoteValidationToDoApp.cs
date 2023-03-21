using ToDoApp.DTO.Response;
using ToDoApp.Models;

namespace ToDoApp.Interfaces.Validators
{
    public interface INoteValidationToDoApp
    {
        public Task<bool> IsNoteValid(NoteDto noteDTO);
    }
}

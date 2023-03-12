using ToDoApp.DTO;
using ToDoApp.Interfaces;
using ToDoApp.Interfaces.Repositories;
using ToDoApp.Interfaces.Validators;
using ToDoApp.Models;

namespace ToDoApp.Validation
{
    public class NoteValidation : INoteValidationToDoApp
    {
        private readonly IUserRepository _userRepository;

        public NoteValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool IsNoteValid(NoteDTO noteDTO)
        {
            return true;
        }
    }
}

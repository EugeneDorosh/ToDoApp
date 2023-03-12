using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Interfaces.Validators
{
    public interface IUserValidationToDoApp
    {
        public bool IsUserValid(UserDTO userDTO);
    }
}

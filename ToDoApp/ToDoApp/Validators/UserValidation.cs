using ToDoApp.DTO;
using ToDoApp.Models;
using ToDoApp.Interfaces.Validators;

namespace ToDoApp.Validation
{
    public class UserValidation : IUserValidationToDoApp
    {
        public bool IsUserValid(UserDTO userDTO)
        {
            if (userDTO == null)
                return false;

            if (userDTO.Username == null)
                return false;

            return true;
        }
    }
}

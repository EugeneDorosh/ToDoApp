using ToDoApp.DTO.Response;
using ToDoApp.Models;

namespace ToDoApp.Interfaces.Validators
{
    public interface IUserValidationToDoApp
    {
        public Task<bool> IsUserValidAsync(UserDto userDTO);
        public Task<bool> IsUserValidAsync(CreateUserDto userDto);
    }
}

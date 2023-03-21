using ToDoApp.DTO.Response;
using ToDoApp.Models;

namespace ToDoApp.Interfaces.Validators
{
    public interface ITaskValidationToDoApp
    {
        public Task<bool> IsTaskValid(ToDoTaskDto taskDTO);
    }
}

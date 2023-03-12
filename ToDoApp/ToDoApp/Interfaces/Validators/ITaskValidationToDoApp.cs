using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Interfaces.Validators
{
    public interface ITaskValidationToDoApp
    {
        public bool IsTaskValid(ToDoTaskDTO taskDTO);
    }
}

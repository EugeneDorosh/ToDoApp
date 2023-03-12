using ToDoApp.DTO;
using ToDoApp.Interfaces;
using ToDoApp.Interfaces.Validators;

namespace ToDoApp.Validators
{
    public class TaskValidation : ITaskValidationToDoApp
    {
        public bool IsTaskValid(ToDoTaskDTO taskDTO)
        {
            if (taskDTO == null) 
                return false;

            if (taskDTO.DateTime <= DateTime.Now)
                return false;

            if ((int)taskDTO.Priority is < 0 or > 2)
                return false;

            if ((int)taskDTO.Status is < 0 or > 1)
                return false;

            return true;
        }
    }
}

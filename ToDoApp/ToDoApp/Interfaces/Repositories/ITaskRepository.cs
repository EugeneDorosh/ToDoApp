using ToDoApp.Models;
using ToDoApp.Models.Enums;

namespace ToDoApp.Interfaces
{
    public interface ITaskRepository : IBaseRepository
    {
        public ToDoTask GetTask(Guid id);
        public ICollection<ToDoTask> GetTasks(Guid userId);
        public bool CreateTask(ToDoTask task);
        public bool UpdateTask(ToDoTask task);
        public bool DeleteTask(Guid id);
        public bool ChangeStatus(Guid id, Status status);
        public bool TaskExists(Guid id);
    }
}

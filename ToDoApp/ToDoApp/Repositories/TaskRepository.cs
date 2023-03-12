using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Interfaces;
using ToDoApp.Interfaces.Repositories;
using ToDoApp.Models;
using ToDoApp.Models.Enums;

namespace ToDoApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoContext _context;

        public TaskRepository(ToDoContext context)
        {
            _context = context;
        }
        public bool ChangeStatus(Guid id, Status status)
        {
            throw new NotImplementedException();
        }

        public bool CreateTask(ToDoTask task)
        {
            _context.Add(task);
            return Save();
        }

        public bool DeleteTask(Guid id)
        {
            ToDoTask taskToDelete = _context.Tasks.FirstOrDefault(t => t.Id == id);
            _context.Tasks.Remove(taskToDelete);
            return Save();
        }

        public ToDoTask GetTask(Guid id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public ICollection<ToDoTask> GetTasks(Guid userId)
        {
            return _context.Tasks.Where(t => t.UserId == userId).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool TaskExists(Guid id)
        {
            return _context.Tasks.Any(t => t.Id == id);
        }

        public bool UpdateTask(ToDoTask task)
        {
            _context.Update(task);
            return Save();
        }
    }
}

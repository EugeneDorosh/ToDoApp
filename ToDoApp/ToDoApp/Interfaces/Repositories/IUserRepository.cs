using ToDoApp.Data;
using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        public User GetUser(Guid id);
        public ICollection<User> GetUsers();
        public bool CreateUser(User user);
        public bool UpdateUser(User user);
        public bool DeleteUser(User user);
        public bool UserExists(Guid id);
    }
}

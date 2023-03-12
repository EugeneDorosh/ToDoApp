using ToDoApp.Data;
using ToDoApp.DTO;
using ToDoApp.Interfaces.Repositories;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoContext _context;

        public UserRepository(ToDoContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            if (!IsUsernameUnique(user.Username))
                return false;

            if (UserExists(user.Id))
                return false;

            _context.Add(user);

            return Save();
        }
        public bool DeleteUser(User user)
        {
            if (!UserExists(user.Id))
                return false;

            _context.Remove(user);
            return Save();
        }

        public User GetUser(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            if (!UserExists(user.Id))
                return false;

            _context.Update(user);
            return Save();
        }

        public bool UserExists(Guid id)
        {
            return _context.Users.Any(x => x.Id == id);
        }

        public bool IsUsernameUnique(string username)
        {
            return !_context.Users.Any(x => x.Username == username);
        }
    }
}

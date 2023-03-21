using ToDoApp.Data;
using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        public Task<User> GetUserAsync(Guid id);
        public Task<ICollection<User>> GetUsersAsync();
        public Task<bool> CreateUserAsync(User user);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(Guid userId);
        public Task<bool> UserExistsAsync(Guid id);
        public Task<bool> IsUsernameUnique(User user);
        public Task<bool> IsUsernameUnique(string username);
    }
}

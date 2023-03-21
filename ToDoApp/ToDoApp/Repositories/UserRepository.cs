﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CreateUserAsync(User user)
        {
            bool isUsernameUnique = await IsUsernameUnique(user.Username);
            if (!isUsernameUnique)
                return false;

            await _context.AddAsync(user);

            return await SaveAsync();
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            bool doesUserExist = await UserExistsAsync(userId);
            if (!doesUserExist)
                return false;

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            _context.Remove(user);

            return await SaveAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            bool doesUserExist = await UserExistsAsync(user.Id);
            if (!doesUserExist)
                return false;

            bool isUsernameUnique = await IsUsernameUnique(user.Username);
            if (!isUsernameUnique)
                return false;

            _context.Update<User>(user);
            return await SaveAsync();
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }

        //for creating user
        public async Task<bool> IsUsernameUnique(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }
        
        //for updating user
        public async Task<bool> IsUsernameUnique(User user)
        {
            User userFromDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userFromDb.Username == user.Username)
                return true;

            return await IsUsernameUnique(user.Username);
        }
    }
}

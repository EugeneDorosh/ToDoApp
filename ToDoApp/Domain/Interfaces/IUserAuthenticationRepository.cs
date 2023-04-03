﻿using Domain.DTO.Request;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DTO.Response;
using ToDoApp.Models;

namespace Domain.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        public Task<IdentityResult> RegisterUserAsync(User userToRegister, UserRegistrationDto userToRegistrate);
        public Task<bool> ValidateUserAsync(UserLoginDto loginDto);
        public Task<string> CreateTokenAsync();
    }
}

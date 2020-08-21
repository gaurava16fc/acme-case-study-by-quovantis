﻿using ACME.Backend.Core.Entities.Models;
using System.Threading.Tasks;

namespace ACME.Backend.Core.Interfaces
{
    public interface IAuthRepository
    {
        //Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}

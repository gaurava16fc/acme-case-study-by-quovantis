﻿
using ACME.Backend.Core.Entities.Models;
using ACME.Backend.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ACME.Backend.Core.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public RepositoryDBContext _context { get; }

        public AuthRepository(RepositoryDBContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null) return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;
            int uid = user.UserRoleId;
            user.UserRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Id == uid);
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (passwordHash[i] != computedHash[i])
                        return false;
                }
            }
            return true;
        }

        //public async Task<User> Register(User user, string password)
        //{
        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(password, out passwordHash, out passwordSalt);
        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;
        //    await _context.Users.AddAsync(user);
        //    await _context.SaveChangesAsync();
        //    return user;
        //}

        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
                return true;
            return false;
        }
    }
}

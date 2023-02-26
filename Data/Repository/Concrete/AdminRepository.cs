using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;
using Data.Repository.Abstract;

namespace Data.Repository.Concrete
{
    public class AdminRepository : IAdminRepository
    {
        public Admin GetByUserNameAndPassword(string username, string password)
        {
            return DbContent.Admins.FirstOrDefault(s => s.Username.ToLower() == username.ToLower() && PasswordHasher.Decyrpt(s.Password) == password);
        }
    }
}


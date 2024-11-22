using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Models;
using Users.ViewModels;

namespace Users.Common
{
    public interface IUserRepository
    {
        void CreateTableUser();
        IEnumerable<User> GetUsers();
        Task<bool> AddUser(User user);
        Task<bool> DeleteUser(Guid id);
        Task<bool> ChangeUser(UserVM user);
    }
}
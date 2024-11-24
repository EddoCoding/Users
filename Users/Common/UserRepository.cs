using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;
using Users.ViewModels;

namespace Users.Common
{
    public class UserRepository(DataContext dbContext) : IUserRepository
    {
        public void CreateTableUser()
        {
            dbContext.Database.EnsureCreated();
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return dbContext.Users.ToArray();
            }
            catch
            {
                return Enumerable.Empty<User>();
            }
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteUser(Guid id)
        {
            try
            {
                await dbContext.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ChangeUser(UserVM userVM)
        {
            try
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userVM.Id);

                if (user != null)
                {
                    user.SurName = userVM.SurName;
                    user.Name = userVM.Name;
                    user.Login = userVM.Login;
                    user.Password = userVM.Password;
                    user.Mail = userVM.Mail;
                    user.AccessLevel = userVM.AccessLevel;
                    user.Notes = userVM.Notes;

                    await dbContext.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
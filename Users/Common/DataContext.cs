using Microsoft.EntityFrameworkCore;
using System.IO;
using Users.Models;

namespace Users.Common
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDataBase.db;");
        }
    }
}
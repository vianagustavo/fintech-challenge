using FintechChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace FintechChallenge.Database
{
    public class DatabaseContext : DbContext
    {

        public DbSet<People> People { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

    }
}
using Microsoft.EntityFrameworkCore;

namespace FintechChallenge.Database
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

    }
}
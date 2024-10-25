using CopitosCase.Models;
using Microsoft.EntityFrameworkCore;

namespace CopitosCase.Database
{
    public class CopitosDbContext : DbContext
    {
        public CopitosDbContext(DbContextOptions<CopitosDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
    }
}

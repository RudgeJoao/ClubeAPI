using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Client> Client { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Rent?> Rents { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace bas.website.Models.Data
{
    public class BankDbContext : DbContext
    {
        private string _connectionString;

        public DbSet<Bank_client> Bank_client { get; set; }
        public DbSet<Bank_client_company> Bank_client_company { get; set; }
        public DbSet<Bank_client_history> Bank_client_history { get; set; }
        public DbSet<Bank_currency> Bank_currency { get; set; }
        public DbSet<Bank_status_history> Bank_status_history { get; set; }


        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options){ }
        public BankDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}

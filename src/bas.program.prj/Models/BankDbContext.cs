using bas.program.Models.Tables.UserTables;
using Microsoft.EntityFrameworkCore;

namespace bas.website.Models.Data
{
    public class BankDbContext : DbContext
    {
        private readonly string _connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Database = BANK; Persist Security Info = False; MultipleActiveResultSets = True; Trusted_Connection = True;";

        #region Данные пользовтеля

        public DbSet<Bank_user> Bank_user { get; set; }

        public DbSet<Bank_user_access> Bank_user_access { get; set; }

        public DbSet<Bank_user_status> Bank_user_status { get; set; }

        #endregion Данные пользовтеля

        #region Данные пользователя

        public DbSet<Bank_client> Bank_client { get; set; }
        public DbSet<Bank_client_company> Bank_client_company { get; set; }
        public DbSet<Bank_client_history> Bank_client_history { get; set; }
        public DbSet<Bank_status_history> Bank_status_history { get; set; }

        #endregion Данные пользователя

        #region Прочие данные

        public DbSet<Bank_currency> Bank_currency { get; set; }

        #endregion Прочие данные

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }

        public BankDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
using bas.program.Models.Tables;
using bas.program.Models.Tables.Active;
using bas.program.Models.Tables.Passive;
using bas.program.Models.Tables.UserTables;
using Microsoft.EntityFrameworkCore;

namespace bas.website.Models.Data
{
    public class BankDbContext : DbContext
    {

        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        private readonly string _connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Database = BANK; Persist Security Info = False; MultipleActiveResultSets = True; Trusted_Connection = True;";

        #region Данные пользователя

        /// <summary>
        /// Пользователи системы
        /// </summary>
        public DbSet<Bank_user> Bank_user { get; set; }

        /// <summary>
        /// Доступы
        /// </summary>
        public DbSet<Bank_user_access> Bank_user_access { get; set; }

        /// <summary>
        /// Статус(Должности)
        /// </summary>
        public DbSet<Bank_user_status> Bank_user_status { get; set; }

        #endregion Данные пользователя

        #region Данные пользователя
        /// <summary>
        /// Клиент банка
        /// </summary>
        public DbSet<Bank_client> Bank_client { get; set; }
        /// <summary>
        /// Информация об компании клиента
        /// </summary>
        public DbSet<Bank_client_company> Bank_client_company { get; set; }
        /// <summary>
        /// Иформация кредитов клиента
        /// </summary>
        public DbSet<Bank_client_history> Bank_client_history { get; set; }
        /// <summary>
        /// Статус истории
        /// </summary>
        public DbSet<Bank_status_history> Bank_status_history { get; set; }

        #endregion Данные пользователя

        #region Прочие данные

        /// <summary>
        /// Курс ресурсов
        /// </summary>
        public DbSet<Bank_currency> Bank_currency { get; set; }

        /// <summary>
        /// Информация о таблицах
        /// </summary>
        public DbSet<Bank_tables_info> Bank_tables_info { get; set; }

        #endregion Прочие данные

        #region Актив

        /// <summary>
        /// Актив - Имущество банка
        /// </summary>
        public DbSet<Bank_active_asset> Bank_active_asset { get; set; }

        /// <summary>
        /// Актив - Уставной капитал банка 
        /// </summary>
        public DbSet<Bank_active_authorized_capital> Bank_active_authorized_capital { get; set; }

        /// <summary>
        /// Актив - Денежные средства и драгоценные металлы
        /// </summary>
        public DbSet<Bank_active_camp> Bank_active_camp { get; set; }

        /// <summary>
        /// Актив - Выданные кредиты
        /// </summary>
        public DbSet<Bank_active_credits_out> Bank_active_credits_out { get; set; }

        /// <summary>
        /// Актив - Депозиты и иные размещенные средства
        /// </summary>
        public DbSet<Bank_active_deposits> Bank_active_deposits { get; set; }

        /// <summary>
        /// Актив - Ценные бумаги и финансовые вложения
        /// </summary>
        public DbSet<Bank_active_docs> Bank_active_docs { get; set; }

        #endregion

        #region Пассив

        /// <summary>
        /// Пассив - Добавочный капитал
        /// </summary>
        public DbSet<Bank_passive_add_capital> Bank_passive_add_capital { get; set; }

        /// <summary>
        /// Пассив - Уставной капитал банка 
        /// </summary>
        public DbSet<Bank_passive_authorized_capital> Bank_passive_authorized_capital { get; set; }

        /// <summary>
        /// Пассив - Денежные средства и драгоценные металлы
        /// </summary>
        public DbSet<Bank_passive_camp> Bank_passive_camp { get; set; }

        /// <summary>
        /// Пассив - Корреспондентские счета коммерческих банков, открытые в банке
        /// </summary>
        public DbSet<Bank_passive_corres_accouts> Bank_passive_corres_accouts { get; set; }

        /// <summary>
        /// Пассив - Выданные кредиты
        /// </summary>
        public DbSet<Bank_passive_credit_debit> Bank_passive_credit_debit { get; set; }

        /// <summary>
        /// Пассив - Депозиты и иные привлеченные средства
        /// </summary>
        public DbSet<Bank_passive_deposits> Bank_passive_deposits { get; set; }

        #endregion

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
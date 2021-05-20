using System.ComponentModel.DataAnnotations;

namespace bas.program.Models.Tables
{
    /// <summary>
    /// Модель с описанием "Информация о таблицах в БД"
    /// </summary>
    public class Bank_tables_info
    {
        [Key]
        public int Tables_id { get; set; }

        public string Tables_name { get; set; }

        public string Tables_descr { get; set; }

        public string Tables_key { get; set; }

        public bool Tables_isSystem { get; set; }
    }
}
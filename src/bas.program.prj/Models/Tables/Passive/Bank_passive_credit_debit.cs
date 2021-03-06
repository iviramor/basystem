using bas.website.Models.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Passive
{
    /// <summary>
    /// Модель с описанием "Выданные кредиты"
    /// </summary>
    public class Bank_passive_credit_debit
    {
        [Key]
        [DisplayName(null)]
        public int Cdebit_id { get; set; }

        [DisplayName("Название операции")]
        public string Cdebit_name { get; set; }

        [DisplayName("Описание")]
        public string Cdebit_describ { get; set; }

        [DisplayName(null)]
        public int Cdebit_lender { get; set; }

        [DisplayName("Сумма кредита")]
        public decimal Cdebit_cash { get; set; }

        [DisplayName(null)]
        public int Cdebit_type { get; set; }

        [ForeignKey("Cdebit_lender")]
        [DisplayName("Заемщик депозита")]
        public virtual Bank_client Bank_client { get; set; }

        [ForeignKey("Cdebit_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }
    }
}
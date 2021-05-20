using bas.website.Models.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Passive
{
    /// <summary>
    /// Модель с описанием "Уставной капитал банка"
    /// </summary>
    public class Bank_passive_authorized_capital
    {
        [Key]
        [DisplayName(null)]
        public int Apc_id { get; set; }

        [DisplayName("Название операции")]
        public string Apc_name_transactions { get; set; }

        [DisplayName("Описание операции")]
        public string Apc_describtion_transactions { get; set; }

        [DisplayName("Дебет")]
        public decimal Apc_debit { get; set; }

        [DisplayName("Кредит")]
        public decimal Apc_credit { get; set; }

        [DisplayName(null)]
        public int Apc_type { get; set; }

        [ForeignKey("Apc_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }
    }
}
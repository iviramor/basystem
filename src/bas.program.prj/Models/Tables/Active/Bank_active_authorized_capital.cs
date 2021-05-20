using bas.website.Models.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Active
{
    /// <summary>
    /// Модель с описанием "Уставной капитал банка"
    /// </summary>
    public class Bank_active_authorized_capital
    {
        [Key]
        [DisplayName(null)]
        public int Aac_id { get; set; }

        [DisplayName("Название операции")]
        public string Aac_name_transactions { get; set; }

        [DisplayName("Описание операции")]
        public string Aac_describtion_transactions { get; set; }

        [DisplayName("Дебет")]
        public decimal Aac_debit { get; set; }

        [DisplayName("Кредит")]
        public decimal Aac_credit { get; set; }

        [DisplayName(null)]
        public int Aac_type { get; set; }

        [ForeignKey("Aac_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }
    }
}
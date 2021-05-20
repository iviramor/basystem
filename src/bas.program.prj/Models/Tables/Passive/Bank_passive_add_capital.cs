using bas.website.Models.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Passive
{
    /// <summary>
    /// Модель с описанием "Добавочный капитал"
    /// </summary>
    public class Bank_passive_add_capital
    {
        [Key]
        [DisplayName(null)]
        public int Addc_id { get; set; }

        [DisplayName("Название операции")]
        public string Addc_name { get; set; }

        [DisplayName("Описание операции")]
        public string Addc_descrip { get; set; }

        [DisplayName("Дебет")]
        public decimal Addc_debit { get; set; }

        [DisplayName("Кредит")]
        public decimal Addc_credit { get; set; }

        [DisplayName(null)]
        public int Addc_type { get; set; }

        [ForeignKey("Addc_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }
    }
}
using bas.website.Models.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Passive
{
    /// <summary>
    /// Модель с описанием "Депозиты и иные привлеченные средства"
    /// </summary>
    public class Bank_passive_deposits
    {
        [Key]
        [DisplayName(null)]
        public int Pas_deposit_id { get; set; }

        [DisplayName("Название депозита")]
        public string Pas_deposit_name { get; set; }

        [DisplayName("Сумма")]
        public decimal Pas_deposit_cash { get; set; }

        [DisplayName(null)]
        public int Pas_deposit_type { get; set; }

        [ForeignKey("Pas_deposit_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }
    }
}
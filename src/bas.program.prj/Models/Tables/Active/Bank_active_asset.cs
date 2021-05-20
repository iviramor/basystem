using bas.website.Models.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Active
{
    /// <summary>
    /// Модель с описанием "Имущество банка"
    /// </summary>
    public class Bank_active_asset
    {
        [Key]
        [DisplayName(null)]
        public int Ass_id { get; set; }

        [DisplayName("Название имущества")]
        public string Ass_name { get; set; }

        [DisplayName("Сумма")]
        public decimal Ass_cash { get; set; }

        [DisplayName(null)]
        public int Ass_type { get; set; }

        [ForeignKey("Ass_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bas.website.Models.Data
{
    /// <summary>
    /// Модель с описанием "Курс ресурсов"
    /// </summary>
    public class Bank_currency
    {
        [Key]
        [DisplayName(null)]
        public int Currency_id { get; set; }

        [MaxLength(100)]
        [DisplayName("Ресурс")]
        public string Currency_name { get; set; }

        [DisplayName("Стоимость в долларах")]
        public decimal Currency_dollar { get; set; }

        [DisplayName("Стоимость в евро")]
        public decimal Currency_euro { get; set; }

        [DisplayName("Стоимость в рублях")]
        public decimal Currency_rub { get; set; }
    }
}
using bas.website.Models.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Active
{
    public class Bank_active_asset
    {
        [Key]
        [DisplayName(null)]
        public int Ass_id { get; set; }

        [DisplayName("Название имущества")]
        public string Ass_name { get; set; }

        [DisplayName(null)]
        public int Ass_cash { get; set; }

        [ForeignKey("Ass_cash")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }


    }
}

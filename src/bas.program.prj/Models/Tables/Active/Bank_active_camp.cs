using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Models.Tables.Active
{
    /// <summary>
    /// Модель с описанием "Денежные средства и драгоценные металлы"
    /// </summary>
    public class Bank_active_camp
    {

        [Key]
        [DisplayName(null)]
        public int Acamp_id { get; set; }

        [DisplayName("Название средства")]
        public string Acamp_name { get; set; }

        [DisplayName("Количественный показатель")]
        public decimal Acamp_quantity { get; set; }

        [DisplayName(null)]
        public int Acamp_type { get; set; }

        [ForeignKey("Acamp_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

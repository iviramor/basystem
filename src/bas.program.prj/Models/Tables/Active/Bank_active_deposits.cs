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
    /// Модель с описанием "Депозиты и иные размещенные средства"
    /// </summary>
    public class Bank_active_deposits
    {

        [Key]
        [DisplayName(null)]
        public int Act_deposit_id { get; set; }

        [DisplayName("Название депозита")]
        public string Act_deposit_name { get; set; }

        [DisplayName("Сумма")]
        public decimal Act_deposit_cash { get; set; }

        [DisplayName(null)]
        public int Act_deposit_type { get; set; }

        [ForeignKey("Act_deposit_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }
    }
}

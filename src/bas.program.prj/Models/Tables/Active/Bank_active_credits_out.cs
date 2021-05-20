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
    /// Модель с описанием "Выданные кредиты"
    /// </summary>
    public class Bank_active_credits_out
    {

        [Key]
        [DisplayName(null)]
        public int Co_id { get; set; }

        [DisplayName("Название операции")]
        public string Co_name { get; set; }

        [DisplayName("Описание операции")]
        public string Co_describ { get; set; }

        [DisplayName(null)]
        public int Co_debtor { get; set; }

        [DisplayName("Сумма кредита")]
        public decimal Co_cash { get; set; }

        [DisplayName(null)]
        public int Co_type { get; set; }

        [ForeignKey("Co_debtor")]
        [DisplayName("Кому заняли")]
        public virtual Bank_client Bank_client { get; set; }

        [ForeignKey("Co_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

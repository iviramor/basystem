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
    public class Bank_active_debit
    {
        [Key]
        [DisplayName(null)]
        public int Cdebit_id { get; set; }

        [DisplayName("Название операции")]
        public string Cdebit_name { get; set; }

        [DisplayName("У кого заняли (банк)")]
        public string Cdebit_lender { get; set; }

        [DisplayName("Сумма дебета")]
        public decimal Cdebit_debit { get; set; }

        [DisplayName(null)]
        public int Cdebit_type { get; set; }

        [ForeignKey("Cdebit_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

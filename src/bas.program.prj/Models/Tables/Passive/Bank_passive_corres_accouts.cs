using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Passive
{
    public class Bank_passive_corres_accouts
    {

        [Key]
        [DisplayName(null)]
        public int Ca_bank_id { get; set; }

        [DisplayName("Наименование счета")]
        public string Ca_bank_name { get; set; }

        [DisplayName("Описание")]
        public string Ca_bank_describ { get; set; }

        [DisplayName(null)]
        public int Ca_bank_company { get; set; }

        [DisplayName("Сумма счет")]
        public decimal Ca_bank_cash { get; set; }

        [DisplayName(null)]
        public int Ca_bank_type { get; set; }

        [ForeignKey("Ca_bank_company")]
        [DisplayName("Клиент")]
        public virtual Bank_client Bank_client { get; set; }

        [ForeignKey("Ca_bank_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

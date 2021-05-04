﻿using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Passive
{
    public class Bank_passive_credit_debit
    {

        [Key]
        [DisplayName(null)]
        public int Cdebit_id { get; set; }

        [DisplayName("Название депозита")]
        public string Cdebit_name { get; set; }

        [DisplayName("Название депозита")]
        public string Cdebit_lender { get; set; }

        [DisplayName("Сумма")]
        public decimal Cdebit_cash { get; set; }

        [DisplayName(null)]
        public int Cdebit_type { get; set; }

        [ForeignKey("Cdebit_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

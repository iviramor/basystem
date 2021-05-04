﻿using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Models.Tables.Passive
{
    public class Bank_passive_authorized_capital
    {

        [Key]
        [DisplayName(null)]
        public int Apc_id { get; set; }

        [DisplayName("Название операции")]
        public string Apc_name_transactions { get; set; }


        [DisplayName("Описание операции")]
        public string Apc_describtion_transactions { get; set; }

        [DisplayName("Дебет")]
        public decimal Apc_debit { get; set; }

        [DisplayName("Кредит")]
        public decimal Apc_credit { get; set; }

        [DisplayName(null)]
        public int Apc_type { get; set; }

        [ForeignKey("Apc_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.Passive
{
    /// <summary>
    /// Модель с описанием "Денежные средства и драгоценные металлы"
    /// </summary>
    public class Bank_passive_camp
    {

        [Key]
        [DisplayName(null)]
        public int Pcamp_id { get; set; }

        [DisplayName("Название операции")]
        public string Pcamp_name { get; set; }

        [DisplayName("Количество средства")]
        public decimal Pcamp_quantity { get; set; }

        [DisplayName(null)]
        public int Pcamp_type { get; set; }

        [ForeignKey("Pcamp_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

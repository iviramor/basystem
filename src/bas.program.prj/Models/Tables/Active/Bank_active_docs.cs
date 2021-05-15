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
    public class Bank_active_docs
    {

        [Key]
        [DisplayName(null)]
        public int Docs_id { get; set; }

        [DisplayName("Наименование документа")]
        public string Docs_name { get; set; }

        [DisplayName("Тип документации(декларация, документ, справка)")]
        public string Docs_type_doc { get; set; }

        [DisplayName("Дебет")]
        public decimal Docs_cash { get; set; }

        [DisplayName(null)]
        public int Docs_type { get; set; }

        [ForeignKey("Docs_type")]
        [DisplayName("Тип средства(гр. Кг. Руб. Дол.)")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

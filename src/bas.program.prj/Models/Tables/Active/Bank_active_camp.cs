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
    public class Bank_active_camp
    {

        [Key]
        [DisplayName(null)]
        public int Acamp_id { get; set; }

        [DisplayName("Название средства")]
        public string Acamp_name { get; set; }

        [DisplayName("Количественный показатель")]
        public string Acamp_quantity { get; set; }

        [DisplayName(null)]
        public string Acamp_type { get; set; }

        [ForeignKey("Acamp_type")]
        [DisplayName("Валюта")]
        public virtual Bank_currency Bank_currency { get; set; }

    }
}

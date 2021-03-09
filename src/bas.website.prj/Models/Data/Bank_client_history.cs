using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.website.Models.Data
{
    public class Bank_client_history
    {
        [Key]
        public int Clihis_id { get; set; }

        public Guid Clihis_numb { get; set; }

        public int Clihis_client { get; set; }

        public decimal Clihis_percent { get; set; }

        public decimal Clihis_all_sum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Clihis_start_date { get; set; }

        public int Clihis_ddl_date { get; set; }

        public decimal Clihis_paid_off { get; set; }

        public decimal Clihis_paid { get; set; }

        public int Clihis_status { get; set; }

        public int Clihis_cur { get; set; }

        [ForeignKey("Clihis_client")]
        public virtual Bank_client Bank_client { get; set; }

        [ForeignKey("clihis_cur")]
        public virtual Bank_currency Bank_currency { get; set; }

        [ForeignKey("Clihis_status")]
        public virtual Bank_status_history Bank_status_history { get; set; }


    }
}

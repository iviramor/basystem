using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.website.Models.Data
{
    public class Bank_status_history
    {


        [Key]
        public int status_id { get; set; }

        public string status_name { get; set; }

        public decimal status_value { get; set; }

    }
}

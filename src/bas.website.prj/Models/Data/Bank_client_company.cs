using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.website.Models.Data
{
    public class Bank_client_company
    {
        [Key]
        public int clcomp_id { get; set; }

        public string Clcomp_name { get; set; }

        public string Clcomp_descr { get; set; }

        public string Clcomp_type { get; set; }
    }
}

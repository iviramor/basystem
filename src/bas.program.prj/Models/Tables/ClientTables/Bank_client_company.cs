using System.ComponentModel.DataAnnotations;

namespace bas.website.Models.Data
{
    public class Bank_client_company
    {
        [Key]
        public int Clcomp_id { get; set; }

        [MaxLength(100)]
        public string Clcomp_name { get; set; }

        public string Clcomp_descr { get; set; }

        [MaxLength(100)]
        public string Clcomp_type { get; set; }
    }
}

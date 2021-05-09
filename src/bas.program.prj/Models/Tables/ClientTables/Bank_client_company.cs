using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bas.website.Models.Data
{
    public class Bank_client_company
    {
        [Key]
        [DisplayName(null)]
        public int Clcomp_id { get; set; }

        [MaxLength(100)]
        [DisplayName("Название компании")]
        public string Clcomp_name { get; set; }

        [DisplayName("Описание компании")]
        public string Clcomp_descr { get; set; }

        [MaxLength(100)]
        [DisplayName("Тип компании")]
        public string Clcomp_type { get; set; }

        [MaxLength(100)]
        [DisplayName("Адрес компании")]
        public string Clcomp_adr { get; set; }

    }
}

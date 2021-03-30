using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.website.Models.Data
{
    public class Bank_client
    {
        [Key]
        public int Client_id { get; set; }

        [MaxLength(50)]
        public string Client_name { get; set; }

        [MaxLength(50)]
        public string Client_surname { get; set; }

        [MaxLength(50)]
        public string Client_patronymic { get; set; }

        public bool Client_sex { get; set; }

        [DisplayFormat(NullDisplayText = "Отсутствует")]
        public int? Client_company { get; set; }

        [MaxLength(30)]
        public string Client_login { get; set; }

        [MaxLength(30)]
        public string Client_password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Client_register_data { get; set; }

        [ForeignKey("Client_company")]
        public virtual Bank_client_company Bank_client_company { get; set; }



    }
}

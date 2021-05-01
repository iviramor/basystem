using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace bas.website.Models.Data
{
    public class Bank_client
    {
        [Key]
        [DisplayName(null)]
        public int Client_id { get; set; }

        [MaxLength(50)]
        [DisplayName("Имя")]
        public string Client_name { get; set; }

        [MaxLength(50)]
        [DisplayName("Фамилия")]
        public string Client_surname { get; set; }

        [MaxLength(50)]
        [DisplayName("Отчество")]
        public string Client_patronymic { get; set; }

        [DisplayName("Пол")]
        public bool Client_sex { get; set; }

        [DisplayFormat(NullDisplayText = "Отсутствует")]
        [DisplayName("Компания")]
        public int? Client_company { get; set; }

        [MaxLength(30)]
        [DisplayName("Логин")]
        public string Client_login { get; set; }

        [MaxLength(30)]
        [DisplayName(null)]
        public string Client_password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName(null)]
        public DateTime Client_register_data { get; set; }

        [ForeignKey("Client_company")]
        [DisplayName(null)]
        public virtual Bank_client_company Bank_client_company { get; set; }

    }
}

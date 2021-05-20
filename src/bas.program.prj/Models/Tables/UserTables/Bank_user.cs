using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.UserTables
{
    /// <summary>
    /// Модель с описанием "Пользователи системы"
    /// </summary>
    public class Bank_user
    {
        [Key]
        public int User_id { get; set; }

        [MaxLength(50)]
        public string User_name { get; set; }

        [MaxLength(50)]
        public string User_surname { get; set; }

        [MaxLength(50)]
        public string User_patronymic { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime User_age { get; set; }

        public bool User_sex { get; set; }

        [MaxLength(30)]
        public string User_login { get; set; }

        [MaxLength(60)]
        public string User_password { get; set; }

        public int User_status_to_system { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime User_register_data { get; set; }

        [ForeignKey("User_status_to_system")]
        public virtual Bank_user_status Bank_user_status { get; set; }
    }
}
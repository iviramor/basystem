using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.UserTables
{
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


        public void ChangeUser(Bank_user oldUser, Bank_user user)
        {
            oldUser.User_id = user.User_id;
            oldUser.User_name = user.User_name;
            oldUser.User_surname = user.User_surname;
            oldUser.User_patronymic = user.User_patronymic;
            oldUser.User_sex = user.User_sex;
            oldUser.User_login = user.User_login;
            oldUser.User_password = user.User_password;
            oldUser.User_status_to_system = user.User_status_to_system;
            oldUser.User_register_data = user.User_register_data;
        }
    }
}

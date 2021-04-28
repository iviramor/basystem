using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bas.program.Models.Tables.UserTables
{
    public class Bank_user_status
    {

        [Key]
        [DisplayName(null)]
        public int? Status_id { get; set; }

        [MaxLength(300)]
        [DisplayName("Статус в системе")]
        public string Status_name { get; set; }
        [DisplayName("Описание")]
        public string Status_describ { get; set; }

        [DisplayName(null)]
        public bool? Status_full_access { get; set; }

        [DisplayName(null)]
        public bool? Status_higher { get; set; }

        [DisplayName("Доступы")]
        public List<Bank_user_access> Bank_user_access { get; set; }

    }
}

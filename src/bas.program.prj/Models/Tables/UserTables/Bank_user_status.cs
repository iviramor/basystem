using System.ComponentModel.DataAnnotations;

namespace bas.program.Models.Tables.UserTables
{
    public class Bank_user_status
    {
        [Key]
        public int Status_id { get; set; }

        [MaxLength(300)]
        public string Status_name { get; set; }

        public string Status_describ { get; set; }

    }
}

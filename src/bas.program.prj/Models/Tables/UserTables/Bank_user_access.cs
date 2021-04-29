using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.UserTables
{
    public class Bank_user_access
    {
        [Key]
        [DisplayName(null)]
        public int Access_id { get; set; }
        [DisplayName("status")]
        public int Access_user_status { get; set; }

        [MaxLength(350)]
        [DisplayName("name_table")]
        public int Access_name_table { get; set; }
        [DisplayName("modification")]
        public int Access_modification { get; set; }

        [ForeignKey("Access_user_status")]
        public virtual Bank_user_status Bank_user_status { get; set; }

        [ForeignKey("Access_name_table")]
        public virtual Bank_tables_info Bank_tables_info { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.program.Models.Tables.UserTables
{
    public class Bank_user_access
    {
        [Key]
        public int Access_id { get; set; }

        public int Access_user_status { get; set; }

        [MaxLength(350)]
        public string Access_name_table { get; set; }

        public int Access_modification { get; set; }

        [ForeignKey("Access_user_status")]
        public virtual Bank_user_status Bank_user_status { get; set; }
    }
}

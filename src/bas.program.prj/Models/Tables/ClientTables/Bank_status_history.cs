using System.ComponentModel.DataAnnotations;

namespace bas.website.Models.Data
{
    public class Bank_status_history
    {


        [Key]
        public int Status_id { get; set; }

        [MaxLength(150)]
        public string Status_name { get; set; }

        public decimal Status_value { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace bas.website.Models.Data
{
    public class Bank_currency
    {
        [Key]
        public int Currency_id { get; set; }
        public string Currency_name { get; set; }
        public decimal Currency_dollar { get; set; }
        public decimal Currency_euro { get; set; }
        public decimal Currency_rub { get; set; }
    }
}

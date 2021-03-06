using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bas.website.Models.Data
{
    public class Bank_client
    {
        [Key]
        public int Client_id { get; set; }
        public string Client_name { get; set; }
        public string Client_surname { get; set; }
        public string Client_patronymic { get; set; }
        public int Client_company { get; set; }
        public string Client_login { get; set; }
        public string Client_password { get; set; }
        //public DataType Client_register_data { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Models.Tables
{
    public class Bank_tables_info
    {
        [Key]
        public int Tables_id { get; set; }

        public string Tables_name { get; set; }

        public string Tables_descr { get; set; }

        public string Tables_key { get; set; }

        public bool Tables_isSystem { get; set; }


    }
}
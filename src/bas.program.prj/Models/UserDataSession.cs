using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bas.program.Models.Tables.UserTables;
using bas.website.Models.Data;

namespace bas.program.Models
{
    public class UserDataSession
    {

        private BankDbContext _DataBase = new BankDbContext();

        private bool _Session;

        private Bank_user _User;


        public Bank_user User
        {
            get => _User;
            set
            {
                _User = value;
            }
        }

        public BankDbContext DataBase
        {
            get => _DataBase;
        }

        public bool Session
        {
            get => _Session;
            set
            {
                _Session = value;
            }
        }



    }
}

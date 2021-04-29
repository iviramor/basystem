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
        /// <summary>
        /// База данных или же Контекст базы данных
        /// </summary>
        private BankDbContext _DataBase = new();
         
        /// <summary>
        /// Поле со статусом сессии пользователя, если не авторизован, то false
        /// Если авторизован то true
        /// </summary>
        private bool _Session;
        /// <summary>
        /// Свойство поля статуса Сессии
        /// </summary>
        public bool Session
        {
            get => _Session;
            set
            {
                _Session = value;
            }
        }


        private Bank_user _User;
        /// <summary>
        /// Свойство пользователя с данными, заполняются при успешной авторизации
        /// </summary>
        public Bank_user User
        {
            get => _User;
            set
            {
                _User = value;
            }
        }


        /// <summary>
        /// Свойство выдает Объект контекста или же базы данных
        /// </summary>
        public BankDbContext DataBase
        {
            get => _DataBase;
        }

    }
}

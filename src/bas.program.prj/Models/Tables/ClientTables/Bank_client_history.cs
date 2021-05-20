using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bas.website.Models.Data
{
    /// <summary>
    /// Модель с описанием "Информация кредитов клиента"
    /// </summary>
    public class Bank_client_history
    {
        [Key]
        [DisplayName(null)]
        public int Clihis_id { get; set; }
        [DisplayName("Код кредита")]
        public Guid Clihis_numb { get; set; }

        [DisplayName(null)]
        public int Clihis_client { get; set; }

        [DisplayName("Процент")]
        public decimal Clihis_percent { get; set; }

        [DisplayName("Всего")]
        public decimal Clihis_all_sum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата начала")]
        public DateTime Clihis_start_date { get; set; }

        [DisplayName("Срок")]
        public int Clihis_ddl_date { get; set; }

        [DisplayName("Заплачено")]
        public decimal Clihis_paid_off { get; set; }

        [DisplayName("Осталось заплатить")]
        public decimal Clihis_paid { get; set; }

        [DisplayName(null)]
        public int Clihis_status { get; set; }

        [DisplayName(null)]
        public int Clihis_cur { get; set; }

        [ForeignKey("Clihis_client")]
        [DisplayName("Клиент")]
        public virtual Bank_client Bank_client { get; set; }

        [ForeignKey("Clihis_cur")]
        [DisplayName("Валюта")]
        public virtual Bank_currency Bank_currency { get; set; }

        [ForeignKey("Clihis_status")]
        [DisplayName("Статус")]
        public virtual Bank_status_history Bank_status_history { get; set; }


    }
}

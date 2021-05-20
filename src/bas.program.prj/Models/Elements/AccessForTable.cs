using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Models.Elements
{
    /// <summary>
    /// Модель с описанием типом доступа
    /// (прим. key:1 - name:полный, 2 - только изменения)
    /// </summary>
    public class AccessForTable
    {
        public string NameAccess { get; set; }
        public int KeyAccess { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bms.Data.Entities
{
    public class Business : EntityBase<int>
    {
        public string Name { get; set; }

        public DateTime DateFounded { get; set; }
    }
}

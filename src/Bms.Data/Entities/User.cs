using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bms.Data.Entities
{
    public class User : EntityBase<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ForeignKey(nameof(Business))]
        public int? BusinessId { get; set; }

        public virtual Business Business { get; set; }

        public bool Active { get; set; }

    }
}

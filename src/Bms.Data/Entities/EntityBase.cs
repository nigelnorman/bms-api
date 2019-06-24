using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bms.Data.Entities
{
    public abstract class EntityBase<TKey>
    {
        [Key, Column(Order = 0)]
        public TKey Id { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

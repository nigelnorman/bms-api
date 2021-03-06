﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sms.Data.Entities
{
    // this allows entities to be configured to use other types for PKs, like guids
    public abstract class EntityBase<TKey>
    {
        [Key, Column(Order = 0)]
        public TKey Id { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

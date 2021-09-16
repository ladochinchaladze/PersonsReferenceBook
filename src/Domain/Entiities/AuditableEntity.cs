﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entiities
{
    public abstract class AuditableEntity
    {
        public DateTime CreateOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}

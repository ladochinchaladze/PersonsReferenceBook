﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ReletivePersonsModel
    {
        public int RelatedPersonId { get; set; }
        public ReletiveTypeEnum ReletiveType { get; set; }
    }
}

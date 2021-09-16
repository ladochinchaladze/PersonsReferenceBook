using Application.Common.Mappings;
using Domain.Entiities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ContactModel : IMapFrom<Contact>
    {
        public string Number { get; set; }
        public ContactTypeEnum ContactType { get; set; }
    }
}

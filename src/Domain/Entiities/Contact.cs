using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entiities
{
    public class Contact : AuditableEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public int PersonId { get; set; }

        public Person Person { get; set; }
    }
}

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entiities
{
    public class PersonRelatedPerson : AuditableEntity
    {
        public int Id { get; set; }
        public ReletiveTypeEnum ReletiveType { get; set; }
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }

        public Person Person { get; set; }
        public Person RelatedPerson { get; set; }
    }
}

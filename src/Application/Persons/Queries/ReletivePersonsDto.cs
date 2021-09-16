using Application.Common.Mappings;
using Domain.Entiities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persons.Queries
{
    public class ReletivePersonsDto : IMapFrom<PersonRelatedPerson>
    {
        public int Id { get; set; }
        public ReletiveTypeEnum ReletiveType { get; set; }
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
    }
}

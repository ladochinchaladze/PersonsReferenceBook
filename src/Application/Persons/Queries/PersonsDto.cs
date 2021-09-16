using Application.Cities.Queries;
using Application.Common.Mappings;
using Application.Common.Models;
using Domain.Entiities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persons.Queries
{
    public class PersonsDto : IMapFrom<Person>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string ImageName { get; set; }

        public CityDto City { get; set; }
        public List<ReletivePersonsDto> PersonRelatedPersons { get; set; }
        public List<ContactModel> Contact { get; set; }
    }
}

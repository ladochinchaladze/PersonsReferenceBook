using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entiities
{
    public class Person : AuditableEntity
    {
        private DateTime _birthDate;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { if (!IsAdult(value)) throw new Exception("Age must be over 18 years"); _birthDate = value; }
        }
        public int CityId { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }

        public List<PersonRelatedPerson> PersonRelatedPersons { get; set; } 
        public List<Contact> Contacts { get; set; }
        public City City { get; set; }


        private bool IsAdult(DateTime birthDate)
        {
            var today = DateTime.Today;

            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age)) age--;

            return age >= 18;
        }
    }
}
